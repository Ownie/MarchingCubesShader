using System;
using GalaSoft.MvvmLight;
using SharpDX;
using SharpDX.Direct3D10;

namespace DirectxWpf.Model
{
    [Serializable]
    public class Settings
    {
        // Variables
        public int SizeX;
        public int SizeY;
        public int SizeZ;
        public float Zoom;
        public int Rotation;
        public float LightDirectionX;
        public float LightDirectionY;
        public float LightDirectionZ;
        public float CellSize;
        public float ColorR;
        public float ColorG;
        public float ColorB;
        public float ColorA;
        public string NoiseMap1Location;
        public string NoiseMap2Location;
        public string NoiseMap3Location;
        public float DensityNormalDiv;
        public bool UseTexture;
        public string TextureNoiseLocation;
        public string TextureLocation;
        public bool UseNormalMap;
        public string NormalMapLocation;
        public float ModU;
        public float ModUOffset;
        public float ModV;
        public bool Clamp;
        public bool UseSpecular;
        public float Shininess;
        public float ColorSpecularR;
        public float ColorSpecularG;
        public float ColorSpecularB;
        public float ColorSpecularA;
        public float AmbientIntensity;
        public float AmbientColorR;
        public float AmbientColorG;
        public float AmbientColorB;
        public float AmbientColorA;
        public float StartDensity;
        public bool UseStartDensity;
        public float Frequency01;
        public float Amplitude01;
        public float Frequency02;
        public float Amplitude02;
        public float Frequency03;
        public float Amplitude03;
        public float GroundLevel;
        public float ForcedGroundLevel;
        public bool ForceGroundLevel;
        public float Sphere;
        public bool Spherical;
    }

    public class SettingsModel : ObservableObject
    {
        // Variables
        private int _sizeX = 20;
        public int SizeX
        {
            get { return _sizeX; }
            set
            {
                if (_sizeX != value)
                {
                    _sizeX = value;
                    RaisePropertyChanged();
                }
            }
        }
        private int _sizeY = 20;
        public int SizeY
        {
            get { return _sizeY; }
            set
            {
                if (_sizeY != value)
                {
                    _sizeY = value;
                    RaisePropertyChanged();
                }
            }
        }
        private int _sizeZ = 20;
        public int SizeZ
        {
            get { return _sizeZ; }
            set
            {
                if (_sizeZ != value)
                {
                    _sizeZ = value;
                    RaisePropertyChanged();
                }
            }
        }

        private float _zoom = 1;
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;

                    RaisePropertyChanged();
                }
            }
        }
        
        private int _rotation = 0;
        public int Rotation
        {
            get { return _rotation; }
            set
            {
                if (_rotation != value)
                {
                    _rotation = value;

                    RaisePropertyChanged();
                }
            }
        }

        // Info
        private Vector3 _lightDirection = Vector3.Down;
        public Vector3 LightDirection
        {
            get { return _lightDirection; }
            set
            {
                if (_lightDirection != value)
                {
                    _lightDirection = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        private float _lightDirectionX = 0;
        public float LightDirectionX
        {
            get { return _lightDirectionX; }
            set
            {
                if (_lightDirectionX != value)
                {
                    _lightDirectionX = value;
                    LightDirection = new Vector3(_lightDirectionX, _lightDirectionY, _lightDirectionZ);
                    LightDirection.Normalize();
                    RaisePropertyChanged();
                }
            }
        }
        private float _lightDirectionY = -1;
        public float LightDirectionY
        {
            get { return _lightDirectionY; }
            set
            {
                if (_lightDirectionY != value)
                {
                    _lightDirectionY = value;
                    LightDirection = new Vector3(_lightDirectionX, _lightDirectionY, _lightDirectionZ);
                    LightDirection.Normalize();
                    RaisePropertyChanged();
                }
            }
        }
        private float _lightDirectionZ = 0;
        public float LightDirectionZ
        {
            get { return _lightDirectionZ; }
            set
            {
                if (_lightDirectionZ != value)
                {
                    _lightDirectionZ = value;
                    LightDirection = new Vector3(_lightDirectionX, _lightDirectionY, _lightDirectionZ);
                    LightDirection.Normalize();
                    RaisePropertyChanged();
                }
            }
        }

        // Voxelsize
        private float _cellSize = 1;
        public float CellSize
        {
            get { return _cellSize; }
            set
            {
                if (_cellSize != value)
                {
                    _cellSize = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // Base color if no texture
        private Vector4 _color = new Vector4(1,1,1,1);
        public Vector4 Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // NoiseMaps
        private string _noiseMap1Location;
        public string NoiseMap1Location
        {
            get { return _noiseMap1Location; }
            set
            {
                if (_noiseMap1Location != value)
                {
                    _noiseMap1Location = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _noiseMap2Location;
        public string NoiseMap2Location
        {
            get { return _noiseMap2Location; }
            set
            {
                if (_noiseMap2Location != value)
                {
                    _noiseMap2Location = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _noiseMap3Location;
        public string NoiseMap3Location
        {
            get { return _noiseMap3Location; }
            set
            {
                if (_noiseMap3Location != value)
                {
                    _noiseMap3Location = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _noiseMap01 = null;
        public ShaderResourceView NoiseMap01
        {
            get { return _noiseMap01; }
            set
            {
                if (_noiseMap01 != value)
                {
                    _noiseMap01 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _noiseMap02 = null;
        public ShaderResourceView NoiseMap02
        {
            get { return _noiseMap02; }
            set
            {
                if (_noiseMap02 != value)
                {
                    _noiseMap02 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _noiseMap03 = null;
        public ShaderResourceView NoiseMap03
        {
            get { return _noiseMap03; }
            set
            {
                if (_noiseMap03 != value)
                {
                    _noiseMap03 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // Density values
        private ShaderResourceView _densityVolume = null;
        public ShaderResourceView DensityVolume
        {
            get { return _densityVolume; }
            set
            {
                if (_densityVolume != value)
                {
                    _densityVolume = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _densityNormalDiv = 0.8f;
        public float DensityNormalDiv
        {
            get { return _densityNormalDiv; }
            set
            {
                if (_densityNormalDiv != value)
                {
                    _densityNormalDiv = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // Texturing
        private bool _useTexture = false;
        public bool UseTexture
        {
            get { return _useTexture; }
            set
            {
                if (_useTexture != value)
                {
                    _useTexture = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private string _textureNoiseLocation;
        public string TextureNoiseLocation
        {
            get { return _textureNoiseLocation; }
            set
            {
                if (_textureNoiseLocation != value)
                {
                    _textureNoiseLocation = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _textureNoise = null;
        public ShaderResourceView TextureNoise
        {
            get { return _textureNoise; }
            set
            {
                if (_textureNoise != value)
                {
                    _textureNoise = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private string _textureLocation;
        public string TextureLocation
        {
            get { return _textureLocation; }
            set
            {
                if (_textureLocation != value)
                {
                    _textureLocation = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _textureMap = null;
        public ShaderResourceView TextureMap
        {
            get { return _textureMap; }
            set
            {
                if (_textureMap != value)
                {
                    _textureMap = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private bool _useNormalMap = false;
        public bool UseNormalMap
        {
            get { return _useNormalMap; }
            set
            {
                if (_useNormalMap != value)
                {
                    _useNormalMap = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private string _normalMapLocation;
        public string NormalMapLocation
        {
            get { return _normalMapLocation; }
            set
            {
                if (_normalMapLocation != value)
                {
                    _normalMapLocation = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ShaderResourceView _normalMap = null;
        public ShaderResourceView NormalMap
        {
            get { return _normalMap; }
            set
            {
                if (_normalMap != value)
                {
                    _normalMap = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _modU = 0.2f;
        public float ModU
        {
            get { return _modU; }
            set
            {
                if (_modU != value)
                {
                    _modU = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _modUOffset = 0;
        public float ModUOffset
        {
            get { return _modUOffset; }
            set
            {
                if (_modUOffset != value)
                {
                    _modUOffset = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _modV = 0.05f;
        public float ModV
        {
            get { return _modV; }
            set
            {
                if (_modV != value)
                {
                    _modV = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private bool _clamp = false;
        public bool Clamp
        {
            get { return _clamp; }
            set
            {
                if (_clamp != value)
                {
                    _clamp = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // Specular
        private bool _useSpecular = false;
        public bool UseSpecular
        {
            get { return _useSpecular; }
            set
            {
                if (_useSpecular != value)
                {
                    _useSpecular = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _shininess = 15;
        public float Shininess
        {
            get { return _shininess; }
            set
            {
                if (_shininess != value)
                {
                    _shininess = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private Vector4 _colorSpecular = new Vector4(1,0.3f,0,1);
        public Vector4 ColorSpecular
        {
            get { return _colorSpecular; }
            set
            {
                if (_colorSpecular != value)
                {
                    _colorSpecular = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        // Ambient
        private float _ambientIntensity = 0.1f;
        public float AmbientIntensity
        {
            get { return _ambientIntensity; }
            set
            {
                if (_ambientIntensity != value)
                {
                    _ambientIntensity = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private Vector4 _ambientColor = new Vector4(-0.5f, -0.5f, -0.5f, 1);
        public Vector4 AmbientColor
        {
            get { return _ambientColor; }
            set
            {
                if (_ambientColor != value)
                {
                    _ambientColor = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        //Adjust look
        private float _startDensity = 0;
        public float StartDensity
        {
            get { return _startDensity; }
            set
            {
                if (_startDensity != value)
                {
                    _startDensity = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private bool _useStartDensity = false;
        public bool UseStartDensity
        {
            get { return _useStartDensity; }
            set
            {
                if (_useStartDensity != value)
                {
                    _useStartDensity = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _frequency01 = 0.05f;
        public float Frequency01
        {
            get { return _frequency01; }
            set
            {
                if (_frequency01 != value)
                {
                    _frequency01 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _amplitude01 = 0.5f;
        public float Amplitude01
        {
            get { return _amplitude01; }
            set
            {
                if (_amplitude01 != value)
                {
                    _amplitude01 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _frequency02 = 0.01f;
        public float Frequency02
        {
            get { return _frequency02; }
            set
            {
                if (_frequency02 != value)
                {
                    _frequency02 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _amplitude02 = 5;
        public float Amplitude02
        {
            get { return _amplitude02; }
            set
            {
                if (_amplitude02 != value)
                {
                    _amplitude02 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _frequency03 = 0.005f;
        public float Frequency03
        {
            get { return _frequency03; }
            set
            {
                if (_frequency03 != value)
                {
                    _frequency03 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _amplitude03 = 15;
        public float Amplitude03
        {
            get { return _amplitude03; }
            set
            {
                if (_amplitude03 != value)
                {
                    _amplitude03 = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _groundLevel = 0;
        public float GroundLevel
        {
            get { return _groundLevel; }
            set
            {
                if (_groundLevel != value)
                {
                    _groundLevel = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _forcedGroundLevel = 0;
        public float ForcedGroundLevel
        {
            get { return _forcedGroundLevel; }
            set
            {
                if (_forcedGroundLevel != value)
                {
                    _forcedGroundLevel = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private bool _forceGroundLevel = false;
        public bool ForceGroundLevel
        {
            get { return _forceGroundLevel; }
            set
            {
                if (_forceGroundLevel != value)
                {
                    _forceGroundLevel = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private float _sphere = 5;
        public float Sphere
        {
            get { return _sphere; }
            set
            {
                if (_sphere != value)
                {
                    _sphere = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        private bool _spherical = false;
        public bool Spherical
        {
            get { return _spherical; }
            set
            {
                if (_spherical != value)
                {
                    _spherical = value;
                    
                    RaisePropertyChanged();
                }
            }
        }
        //

        public void Reset()
        {
            this.SizeX = 20;
            this.SizeY = 20;
            this.SizeZ = 20;
            this.Zoom = 1;
            this.Rotation = 0;
            this.LightDirection = Vector3.Down;
            this.LightDirectionX = 0;
            this.LightDirectionY = -1;
            this.LightDirectionZ = 0;
            this.CellSize = 1;
            this.Color = new Vector4(1,1,1,1);
            this.NoiseMap1Location = string.Empty;
            if (NoiseMap01 != null) this.NoiseMap01.Dispose();
            this.NoiseMap01 = null;
            this.NoiseMap2Location = string.Empty;
            if (NoiseMap02 != null) this.NoiseMap02.Dispose();
            this.NoiseMap02 = null;
            this.NoiseMap3Location = string.Empty;
            if (NoiseMap03 != null) this.NoiseMap03.Dispose();
            this.NoiseMap03 = null;
            this.DensityNormalDiv = 0.8f;
            this.UseTexture = false;
            this.TextureNoiseLocation = string.Empty;
            if (TextureNoise != null) this.TextureNoise.Dispose();
            this.TextureNoise = null;
            this.TextureLocation = string.Empty;
            if (TextureMap != null) this.TextureMap.Dispose();
            this.TextureMap = null;
            this.UseNormalMap = false;
            this.NormalMapLocation = string.Empty;
            if (NormalMap != null) this.NormalMap.Dispose();
            this.NormalMap = null;
            this.ModU = 0.2f;
            this.ModUOffset = 0;
            this.ModV = 0.05f;
            this.Clamp = false;
            this.UseSpecular = false;
            this.Shininess = 25;
            this.ColorSpecular = new Vector4(1,1,1,1);
            this.AmbientIntensity = 0.1f;
            this.AmbientColor = new Vector4(-1,-1,-1,1);
            this.StartDensity = 0;
            this.UseStartDensity = false;
            this.Frequency01 = 0.05f;
            this.Amplitude01 = 0.5f;
            this.Frequency02 = 0.01f;
            this.Amplitude02 = 5;
            this.Frequency03 = 0.005f;
            this.Amplitude03 = 15;
            this.GroundLevel = 0;
            this.ForcedGroundLevel = 0;
            this.ForceGroundLevel = false;
            this.Sphere = 15;
            this.Spherical = false;
        }

        public Settings Serialize()
        {
            Settings set = new Settings()
            {
                SizeX = _sizeX,
                SizeY = _sizeY,
                SizeZ = _sizeZ,
                Zoom = _zoom,
                Rotation = _rotation,
                LightDirectionX = _lightDirection.X,
                LightDirectionY = _lightDirection.Y,
                LightDirectionZ = _lightDirection.Z,
                CellSize = _cellSize,
                ColorR = _color.X, 
                ColorG = _color.Y, 
                ColorB = _color.Z, 
                ColorA = _color.W,
                NoiseMap1Location = _noiseMap1Location,
                NoiseMap2Location = _noiseMap2Location,
                NoiseMap3Location = _noiseMap3Location,
                DensityNormalDiv = _densityNormalDiv,
                UseTexture = _useTexture,
                TextureNoiseLocation = _textureNoiseLocation,
                TextureLocation = _textureLocation,
                UseNormalMap = _useNormalMap,
                NormalMapLocation = _normalMapLocation,
                ModU = _modU,
                ModUOffset = _modUOffset,
                ModV = _modV,
                Clamp = _clamp,
                UseSpecular = _useSpecular,
                Shininess = _shininess,
                ColorSpecularR = _colorSpecular.X,
                ColorSpecularG = _colorSpecular.Y,
                ColorSpecularB = _colorSpecular.Z,
                ColorSpecularA = _colorSpecular.W,
                AmbientIntensity = _ambientIntensity,
                AmbientColorR = _ambientColor.X,
                AmbientColorG = _ambientColor.Y,
                AmbientColorB = _ambientColor.Z,
                AmbientColorA = _ambientColor.W,
                StartDensity = _startDensity,
                UseStartDensity = _useStartDensity,
                Frequency01 = _frequency01,
                Amplitude01 = _amplitude01,
                Frequency02 = _frequency02,
                Amplitude02 = _amplitude02,
                Frequency03 = _frequency03,
                Amplitude03 = _amplitude03,
                GroundLevel = _groundLevel,
                ForcedGroundLevel = _forcedGroundLevel,
                ForceGroundLevel = _forceGroundLevel,
                Sphere = _sphere,
                Spherical = _spherical
            };
            return set;
        }

        public void Deserialize(Settings set)
        {
            this.SizeX = set.SizeX;
            this.SizeY = set.SizeY;
            this.SizeZ = set.SizeZ;
            this.Zoom = set.Zoom;
            this.Rotation = set.Rotation;
            this.LightDirection = new Vector3(set.LightDirectionX, set.LightDirectionY, set.LightDirectionZ);
            this.LightDirectionX = set.LightDirectionX;
            this.LightDirectionY = set.LightDirectionY;
            this.LightDirectionZ = set.LightDirectionZ;
            this.CellSize = set.CellSize;
            this.Color = new Vector4(set.ColorR, set.ColorG, set.ColorB, set.ColorA);
            this.NoiseMap1Location = set.NoiseMap1Location;
            this.NoiseMap2Location = set.NoiseMap2Location;
            this.NoiseMap3Location = set.NoiseMap3Location;
            this.DensityNormalDiv = set.DensityNormalDiv;
            this.UseTexture = set.UseTexture;
            this.TextureNoiseLocation = set.TextureNoiseLocation;
            this.TextureLocation = set.TextureLocation;
            this.UseNormalMap = set.UseNormalMap;
            this.NormalMapLocation = set.NormalMapLocation;
            this.ModU = set.ModU;
            this.ModUOffset = set.ModUOffset;
            this.ModV = set.ModV;
            this.Clamp = set.Clamp;
            this.UseSpecular = set.UseSpecular;
            this.Shininess = set.Shininess;
            this.ColorSpecular = new Vector4(set.ColorSpecularR, set.ColorSpecularG, set.ColorSpecularB, set.ColorSpecularA);
            this.AmbientIntensity = set.AmbientIntensity;
            this.AmbientColor = new Vector4(set.AmbientColorR, set.AmbientColorG, set.AmbientColorB, set.AmbientColorA);
            this.StartDensity = set.StartDensity;
            this.UseStartDensity = set.UseStartDensity;
            this.Frequency01 = set.Frequency01;
            this.Amplitude01 = set.Amplitude01;
            this.Frequency02 = set.Frequency02;
            this.Amplitude02 = set.Amplitude02;
            this.Frequency03 = set.Frequency03;
            this.Amplitude03 = set.Amplitude03;
            this.GroundLevel = set.GroundLevel;
            this.ForcedGroundLevel = set.ForcedGroundLevel;
            this.ForceGroundLevel = set.ForceGroundLevel;
            this.Sphere = set.Sphere;
            this.Spherical = set.Spherical;
        }
    }
}