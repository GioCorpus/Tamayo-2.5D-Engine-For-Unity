 Tamayo 2.5D

The open-source 2.5D animation editor for games, motion design and interactive narratives.  
Think Blender + parallax + depth = future of 2D/3D hybrid.

**Status**: Phase 0 – MVP in development.  
**License**: Apache 2.0  
**Goal**: Become the community-driven tool for 2.5D pipelines.

## Features (Planned MVP – 12 months)
- Native parallax layers (3–5)  
- Real Z-depth sorting & lighting  
- Keyframe timeline (position, scale, rotation, alpha)  
- Import: PNG sequences, SVG  
- Export: JSON + WebGL preview  
- Cross-platform: Windows/Linux/macOS  

## How to contribute
1. Fork & clone.  
2. Build with CMake (instructions coming).  
3. Open issues or PRs – we love bugs and ideas.  

Join Discord: Roadmap: GitHub Projects  

Let's build this together.  
— The Tamayo Team

# Tamayo 2.5D

A 2.5D animation engine built with C++20, featuring parallax rendering, timeline-based animation, and comprehensive import/export capabilities.

## Features

### Core Engine
- **Transform System**: Position, scale, rotation, alpha, and anchor point with matrix generation
- **Keyframe Animation**: Frame-based animation with multiple interpolation types (Linear, EaseIn, EaseOut, Step, CubicBezier)
- **Timeline**: Sorted keyframe container with evaluation and frame/time conversion
- **Layer System**: Hierarchical layer structure with visibility, z-depth, and blend modes
- **Scene Management**: Canvas size, background color, layer management, and playback control
- **Parallax Engine**: Camera offset/zoom with depth-based layer offset and scale

### Renderer
- **Depth Sorting**: Z-depth sorting (back-to-front / front-to-back) with visibility filtering
- **Lighting System**: Ambient + point lights with distance attenuation and depth-based falloff
- **Render Context**: Builds ordered render command list integrating parallax and lighting

### Import/Export
- **PNG Importer**: Single file, sequence, and directory import with auto-detection
- **SVG Importer**: SVG file import with scale options
- **JSON Exporter**: Full scene serialization to JSON format
- **WebGL Exporter**: Generates HTML + JS + JSON for browser-based 2.5D preview
- **Project File**: Native .tamayo binary format with versioned JSON payload

### Editor
- **EditorApp**: Application lifecycle, scene management, playback control, export dispatch
- **TimelinePanel**: Frame/position mapping, zoom, scroll, layer selection
- **LayerPanel**: Layer selection with callbacks for visibility changes
- **Viewport**: Screen/world coordinate transforms, pan/zoom, parallax camera integration

## Project Structure

```
tamayo/
├── CMakeLists.txt              # Root CMake config with FetchContent
├── src/
│   ├── CMakeLists.txt          # Builds tamayo_core library + tamayo executable
│   ├── core/                   # Core engine implementation
│   ├── renderer/               # Renderer components
│   ├── io/                     # Import/export modules
│   ├── editor/                 # Editor components
│   └── main.cpp                # CLI entry point
├── include/
│   └── tamayo/
│       ├── core/               # Core engine headers
│       ├── renderer/           # Renderer headers
│       ├── io/                 # Import/export headers
│       └── editor/             # Editor headers
├── tests/                      # Catch2 test files
├── external/                   # External dependencies (GLFW)
├── .gitignore
├── .clang-format
├── LICENSE
└── README.md
```

## Dependencies

- **CMake 3.20+**: Build system
- **C++20**: Language standard
- **nlohmann/json**: JSON serialization (via FetchContent)
- **GLM**: Mathematics library (via FetchContent)
- **Catch2**: Testing framework (via FetchContent)
- **GLFW**: Window management (via submodule)

## Building

```bash
# Clone the repository
git clone https://github.com/tamayo2d/tamayo.git
cd tamayo

# Initialize submodules
git submodule update --init --recursive

# Build with CMake
cmake -S . -B build -DCMAKE_BUILD_TYPE=Release
cmake --build build

# Run tests
cd build && ctest
```

## Usage

### Command Line Interface

```bash
# Show version
./tamayo --version

# Show help
./tamayo --help

# Open a project file
./tamayo --open project.tamayo

# Export to JSON
./tamayo --export-json scene.json

# Export to WebGL
./tamayo --export-webgl output/
```

### Programmatic API

```cpp
#include "tamayo/core/Scene.h"
#include "tamayo/core/Layer.h"
#include "tamayo/io/JsonExporter.h"

// Create a scene
auto scene = std::make_shared<tamayo::Scene>(1920.0f, 1080.0f);

// Add a layer
auto layer = std::make_shared<tamayo::Layer>("Background");
layer->setImagePath("background.png");
scene->addLayer(layer);

// Export to JSON
tamayo::JsonExporter exporter;
exporter.exportToFile(*scene, "scene.json");
```

## Testing

The project includes comprehensive unit tests using Catch2:

```bash
# Run all tests
cd build && ctest

# Run specific test
./tamayo_tests "[transform]"
```

## License

Apache License 2.0 - See LICENSE file for details.

## Contributing

See CONTRIBUTING.md for contribution guidelines.
