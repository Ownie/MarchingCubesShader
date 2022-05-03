using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using DaeSharpWpf;
using DirectxWpf.Helpers;
using DirectxWpf.Model;
using DirectxWpf.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using SharpDX;
using SharpDX.Direct3D10;
using Color = System.Windows.Media.Color;

namespace DirectxWpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // PUBLIC
        public SettingsModel Settings { get; set; }
        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    RaisePropertyChanged();
                }
            }
        }

        // PRIVATE
        private Device1 _device = null;
        private readonly ChunkViewport _viewport = null;
        private const string _extensionFull = "VoxelProject";
        private const string _extension = ".voxproj";

        public MainViewModel()
        {
            Settings = new SettingsModel();
            Settings.Reset();

            StatusText = "Compiling Light Shader...";
            _viewport = new ChunkViewport(Settings);

            _viewport.InitViewport += InitializedViewPort;
            _viewport.InitEffectLight += InitializedEffectLight;
            _viewport.InitEffectHeavy += InitializedEffectHeavy;
        }

        // Init
        private void InitializedViewPort()
        {
            _device = _viewport.MyDevice;
        }

        private void InitializedEffectLight()
        {
            StatusText = "Compiled Light Shader! Now Compiling Heavy Shader...";
        }

        private void InitializedEffectHeavy()
        {
            StatusText = "Compiled Heavy Shader!";
        }

        // Resize
        private RelayCommand _setSize = null;

        public RelayCommand SetSizeCommand
        {
            get { return _setSize ?? (_setSize = new RelayCommand(SetSize)); }
        }

        private void SetSize()
        {
            var view = new SizeWindow();
            var viewModel = view.DataContext as SizeViewModel;

            if (viewModel != null)
            {
                viewModel.X = Settings.SizeX;
                viewModel.Y = Settings.SizeY;
                viewModel.Z = Settings.SizeZ;
            }

            if (view.ShowDialog() != true) return;

            if (viewModel != null)
            {
                Settings.SizeX = viewModel.X;
                Settings.SizeY = viewModel.Y;
                Settings.SizeZ = viewModel.Z;
            }

            _viewport.Model.Create(_device);
        }

        // OnClose
        private RelayCommand _onClose = null;

        public RelayCommand OnCloseCommand
        {
            get { return _onClose ?? (_onClose = new RelayCommand(OnClose)); }
        }

        private void OnClose()
        {
            if (_viewport.ShaderThread.IsAlive)
                _viewport.ShaderThread.Abort();
        }

        // OnLoad
        private RelayCommand<DX10RenderCanvas> _onLoad = null;

        public RelayCommand<DX10RenderCanvas> OnLoadCommand
        {
            get { return _onLoad ?? (_onLoad = new RelayCommand<DX10RenderCanvas>(OnLoad)); }
        }

        private void OnLoad(DX10RenderCanvas canvas)
        {
            if (_viewport!=null)
            {
                canvas.Viewport = _viewport;
            }
        }

        // Set Color
        private RelayCommand<Color> _setColor = null;

        public RelayCommand<Color> SetColorCommand
        {
            get { return _setColor ?? (_setColor = new RelayCommand<Color>(SetColor)); }
        }

        private void SetColor(Color color)
        {
            Settings.Color = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
        }

        // AmbientColor
        private RelayCommand<Color> _setAmbientColor = null;

        public RelayCommand<Color> SetAmbientColorCommand
        {
            get { return _setAmbientColor ?? (_setAmbientColor = new RelayCommand<Color>(SetAmbientColor)); }
        }

        private void SetAmbientColor(Color color)
        {
            Settings.AmbientColor = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
        }

        // SpecularColor
        private RelayCommand<Color> _setSpecularColor = null;

        public RelayCommand<Color> SetSpecularColorCommand
        {
            get { return _setSpecularColor ?? (_setSpecularColor = new RelayCommand<Color>(SetSpecularColor)); }
        }

        private void SetSpecularColor(Color color)
        {
            Settings.ColorSpecular = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
        }
        
        // LoadNormalMap
        private RelayCommand _loadNormalMap = null;

        public RelayCommand LoadNormalMapCommand
        {
            get { return _loadNormalMap ?? (_loadNormalMap = new RelayCommand(LoadNormalMap)); }
        }

        private void LoadNormalMap()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".dds";
            dlg.Filter = "DDS (*.dds)|*.dds";
            var result = dlg.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            ShaderResourceView res = ShaderResourceView.FromFile(_device, dlg.FileName);

            Settings.NormalMap = res;
            Settings.NormalMapLocation = dlg.FileName;
        }

        // LoadNoiseMap
        private RelayCommand<int> _loadNoiseMap = null;

        public RelayCommand<int> LoadNoiseMapCommand
        {
            get { return _loadNoiseMap ?? (_loadNoiseMap = new RelayCommand<int>(LoadNoiseMap)); }
        }

        private void LoadNoiseMap(int nr)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".dds";
            dlg.Filter = "DDS (*.dds)|*.dds";
            var result = dlg.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            ShaderResourceView res = ShaderResourceView.FromFile(_device, dlg.FileName);

            switch (nr)
            {
                case 1:
                    Settings.NoiseMap01 = res;
                    Settings.NoiseMap1Location = dlg.FileName;
                    break;
                case 2:
                    Settings.NoiseMap02 = res;
                    Settings.NoiseMap2Location = dlg.FileName;
                    break;
                case 3:
                    Settings.NoiseMap03 = res;
                    Settings.NoiseMap3Location = dlg.FileName;
                    break;
            }
        }

        // Load Noise Texture
        private RelayCommand _loadNoiseTexture = null;

        public RelayCommand LoadNoiseTextureCommand
        {
            get { return _loadNoiseTexture ?? (_loadNoiseTexture = new RelayCommand(LoadNoiseTexture)); }
        }

        private void LoadNoiseTexture()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".dds";
            dlg.Filter = "DDS (*.dds)|*.dds";
            var result = dlg.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            ShaderResourceView res = ShaderResourceView.FromFile(_device, dlg.FileName);

            Settings.TextureNoise = res;
            Settings.TextureNoiseLocation = dlg.FileName;
        }

        // Load Texture
        private RelayCommand _loadTexture = null;

        public RelayCommand LoadTextureCommand
        {
            get { return _loadTexture ?? (_loadTexture = new RelayCommand(LoadTexture)); }
        }

        private void LoadTexture()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".dds";
            dlg.Filter = "DDS (*.dds)|*.dds";
            var result = dlg.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            ShaderResourceView res = ShaderResourceView.FromFile(_device, dlg.FileName);

            Settings.TextureMap = res;
            Settings.TextureLocation = dlg.FileName;
        }

        // Rotation
        private RelayCommand<int> _rotation = null;

        public RelayCommand<int> RotationCommand
        {
            get { return _rotation ?? (_rotation = new RelayCommand<int>(Rotation)); }
        }

        private void Rotation(int rotation)
        {
            switch (rotation)
            {
                case -1:
                    if (Settings.Rotation > -10)
                        Settings.Rotation -= 1;
                    break;
                case 0:
                    Settings.Rotation = 0;
                    break;
                case 1:
                    if (Settings.Rotation < 10)
                        Settings.Rotation += 1;
                    break;
            }
        }
        
        // Zoom
        private RelayCommand _scaleUp = null;

        public RelayCommand ScaleUpCommand
        {
            get { return _scaleUp ?? (_scaleUp = new RelayCommand(ScaleUp)); }
        }

        private void ScaleUp()
        {
            _viewport.EyeLocation *= 0.85f;
        }

        // Zoom
        private RelayCommand _scaleDown = null;

        public RelayCommand ScaleDownCommand
        {
            get { return _scaleDown ?? (_scaleDown = new RelayCommand(ScaleDown)); }
        }

        private void ScaleDown()
        {
            _viewport.EyeLocation *= 1.15f;
        }

        // New
        private RelayCommand _new = null;

        public RelayCommand NewCommand
        {
            get { return _new ?? (_new = new RelayCommand(New)); }
        }

        private void New()
        {
            var msg = MessageBox.Show("All unsaved progress will be lost.", "Are you sure?",
                MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);

            if (msg == MessageBoxResult.OK)
            {
                Settings.Reset();
                _viewport.Model.Create(_device);
            }
        }

        // Save
        private RelayCommand _save = null;

        public RelayCommand SaveCommand
        {
            get { return _save ?? (_save = new RelayCommand(Save)); }
        }

        private void Save()
        {
            // Get save location
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = _extension;
            dlg.Filter = $"{_extensionFull} (*{_extension})|*{_extension}";
            var result = dlg.ShowDialog();

            if (!result.HasValue || !result.Value)
                return;

            var saveLoc = dlg.FileName;
            
            using (var stream = File.Create(saveLoc))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, Settings.Serialize());
            }

            ObjExporter.SaveModel(_viewport.GSData, saveLoc.Replace(_extension, ".obj"));
        }

        // Load
        private RelayCommand _load = null;

        public RelayCommand LoadCommand
        {
            get { return _load ?? (_load = new RelayCommand(Load)); }
        }

        private void Load()
        {
            var msg = MessageBox.Show("All unsaved progress will be lost.", "Are you sure?",
                MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
            if (msg == MessageBoxResult.Cancel)
                return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = _extension;
            dlg.Filter = $"{_extensionFull} (*{_extension})|*{_extension}";
            var result = dlg.ShowDialog();

            string source = String.Empty;

            if (result.HasValue && result.Value)
                source = dlg.FileName;
            else
                return;

            using (Stream stream = File.OpenRead(source))
            {
                var formatter = new BinaryFormatter();
                Settings.Deserialize(formatter.Deserialize(stream) as Settings);
                _viewport.Model.Create(_device);
            }

            if (!string.IsNullOrEmpty(Settings.TextureLocation) && File.Exists(Settings.TextureLocation))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.TextureLocation);
                Settings.TextureMap = res;
            }
            else{Settings.TextureLocation = "";}
            if (!string.IsNullOrEmpty(Settings.TextureNoiseLocation) && File.Exists(Settings.TextureNoiseLocation))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.TextureNoiseLocation);
                Settings.TextureNoise = res;
            }
            else{Settings.TextureNoiseLocation = "";}

            if (!string.IsNullOrEmpty(Settings.NormalMapLocation) && File.Exists(Settings.NormalMapLocation))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.NormalMapLocation);
                Settings.NormalMap = res;
            }
            else { Settings.NormalMapLocation = ""; }

            if (!string.IsNullOrEmpty(Settings.NoiseMap1Location) && File.Exists(Settings.NoiseMap1Location))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.NoiseMap1Location);
                Settings.NoiseMap01 = res;
            }
            else{Settings.NoiseMap1Location = "";}
            if (!string.IsNullOrEmpty(Settings.NoiseMap2Location) && File.Exists(Settings.NoiseMap2Location))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.NoiseMap2Location);
                Settings.NoiseMap02 = res;
            }
            else{Settings.NoiseMap2Location = "";}
            if (!string.IsNullOrEmpty(Settings.NoiseMap3Location) && File.Exists(Settings.NoiseMap3Location))
            {
                ShaderResourceView res = ShaderResourceView.FromFile(_device, Settings.NoiseMap3Location);
                Settings.NoiseMap03 = res;
            }
            else{Settings.NoiseMap3Location = "";}
        }
    }
}