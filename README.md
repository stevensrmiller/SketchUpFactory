SketchUpFactory
===============
A C# library for making simple models and saving them as SketchUp files. To compile and run the examples requires
the [CsharpSketchUpAPI][wrapper] wrapper.

Contact us at info@exlumina.com for more information.

Features
--------
The library is primarily for the purpose of creating models for use in Unity (but can be used for any other purpose).
Accordingly, it supports these SketchUp features:

- Creation of polygons, with or without holes in them.
- Collecting polygons into groups.
- Component definitions (groups you can instantiate).
- Component instances.
- Texture mapping.
- Hierarchical modeling.

Notes
-----

To use the library you only need to know how to program in C#. The object model is loosely based on the SketchUp document
object model. But, you need not know anything about the SketchUp model, nor how to manipulate it, to use the library.

See the SketchUpFactoryExamples folder for sample client code.

[wrapper]:
  https://github.com/stevensrmiller/CsharpSketchUpAPI
