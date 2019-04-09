## v1.0.2
- added CHANGELOG
- extracted interface IReactiveDetector for extendability
- extracted base class ReactiveDetectorBase for extendability
- added DisposedBy extension method using CompositeDisposable
- added HandsDetector - now you can do stuff with both hands!
- BUG!!! - if hand leaves scene, last value is sent down the pipeline