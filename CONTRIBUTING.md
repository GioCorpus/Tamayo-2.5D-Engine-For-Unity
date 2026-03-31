# Contributing to Tamayo 2.5D

Thank you for your interest in contributing to Tamayo 2.5D! This document provides guidelines and information for contributors.

## Code Style

### C++ Style Guide

- Follow the `.clang-format` configuration
- Use C++20 features where appropriate
- Prefer `std::shared_ptr` for shared ownership
- Use `const` wherever possible
- Keep functions small and focused
- Write meaningful comments for complex logic

### Naming Conventions

- **Classes**: PascalCase (e.g., `Transform`, `Layer`, `Scene`)
- **Functions**: camelCase (e.g., `setPosition`, `getTransform`)
- **Variables**: camelCase (e.g., `position`, `scale`)
- **Constants**: UPPER_SNAKE_CASE (e.g., `MAX_FRAMES_IN_FLIGHT`)
- **Namespaces**: lowercase (e.g., `tamayo`)

### File Organization

- Header files in `include/tamayo/`
- Implementation files in `src/`
- Test files in `tests/`
- One class per file (with exceptions for small related classes)

## Development Workflow

### Setting Up Development Environment

1. Fork the repository
2. Clone your fork:
   ```bash
   git clone https://github.com/your-username/tamayo.git
   cd tamayo
   ```
3. Initialize submodules:
   ```bash
   git submodule update --init --recursive
   ```
4. Create a build directory:
   ```bash
   cmake -S . -B build -DCMAKE_BUILD_TYPE=Debug
   cmake --build build
   ```

### Making Changes

1. Create a feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
2. Make your changes
3. Write or update tests
4. Run tests:
   ```bash
   cd build && ctest
   ```
5. Format code:
   ```bash
   clang-format -i src/**/*.cpp include/**/*.h
   ```

### Commit Guidelines

- Use clear, descriptive commit messages
- Reference issues in commit messages (e.g., "Fix #123: Add parallax support")
- Keep commits focused and atomic
- Write commit messages in present tense

Example:
```
Add timeline evaluation with cubic bezier interpolation

- Implement cubic bezier curve evaluation
- Add bezier handle support to Keyframe class
- Update Timeline::evaluate() to handle bezier interpolation
- Add tests for bezier interpolation

Fixes #45
```

### Pull Request Process

1. Update documentation if needed
2. Ensure all tests pass
3. Update CHANGELOG.md if applicable
4. Create a pull request with:
   - Clear title describing the change
   - Detailed description of changes
   - Reference to related issues
   - Screenshots or examples if applicable

## Testing

### Writing Tests

- Use Catch2 for unit tests
- Test one thing per test case
- Use descriptive test names
- Test edge cases and error conditions
- Keep tests independent

Example:
```cpp
TEST_CASE("Transform position", "[transform]") {
    Transform transform;
    transform.setPosition(10.0f, 20.0f, 30.0f);
    REQUIRE(transform.getX() == 10.0f);
    REQUIRE(transform.getY() == 20.0f);
    REQUIRE(transform.getZ() == 30.0f);
}
```

### Running Tests

```bash
# Run all tests
cd build && ctest

# Run specific test suite
./tamayo_tests "[transform]"

# Run with verbose output
./tamayo_tests -v
```

## Documentation

### Code Documentation

- Use Doxygen-style comments for public APIs
- Document parameters, return values, and exceptions
- Provide usage examples for complex functions

Example:
```cpp
/**
 * @brief Evaluates the transform at a specific frame
 * 
 * @param frame The frame number to evaluate
 * @return Transform The interpolated transform at the frame
 * 
 * @throws std::out_of_range If frame is outside timeline range
 * 
 * Example:
 * @code
 * Timeline timeline;
 * Transform result = timeline.evaluate(10);
 * @endcode
 */
Transform evaluate(uint32_t frame) const;
```

### README Updates

- Update README.md for new features
- Add usage examples
- Update build instructions if needed

## Issue Reporting

### Bug Reports

Include:
- Operating system and version
- Compiler and version
- Steps to reproduce
- Expected behavior
- Actual behavior
- Error messages or logs
- Minimal code example if applicable

### Feature Requests

Include:
- Use case description
- Proposed API or behavior
- Examples of similar features in other projects
- Mockups or diagrams if applicable

## Code Review

### Review Process

- All changes require review
- Reviewers should check:
  - Code style and formatting
  - Test coverage
  - Documentation
  - Performance implications
  - Security considerations

### Review Checklist

- [ ] Code follows style guidelines
- [ ] Tests are included and pass
- [ ] Documentation is updated
- [ ] No compiler warnings
- [ ] No memory leaks
- [ ] Performance is acceptable

## Release Process

### Versioning

We use [Semantic Versioning](https://semver.org/):
- MAJOR: Incompatible API changes
- MINOR: New functionality in a backwards compatible manner
- PATCH: Backwards compatible bug fixes

### Release Checklist

- [ ] All tests pass
- [ ] Documentation is up to date
- [ ] CHANGELOG.md is updated
- [ ] Version number is updated
- [ ] Release notes are prepared

## Getting Help

- Open an issue for bugs or feature requests
- Join our community discussions
- Check existing documentation

## License

By contributing, you agree that your contributions will be licensed under the Apache License 2.0.
