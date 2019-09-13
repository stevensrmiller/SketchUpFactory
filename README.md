SketchUpFactory
===============
A C# library for making simple models and saving them as SketchUp files. At this time, the source is provided for review
and any other licensed use. To compile and run the examples requires additional libraries not yet public. Contact us at
steve@millerhousehold.com for information on how to obtain those libraries (we hope to make them public here on github
in the near future).

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

To use the library you only need to know how to program in C#. The object model is loosly based on the SketchUp document
object model. But, you need not know anything about the SketchUp model, nor how to manipulate it, to use the library.

See the SketchUpFactoryExamples folder for sample client code.
