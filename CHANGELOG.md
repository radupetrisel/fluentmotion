## v1.0.7
- added [UsedImplicitly] attribute to observable extensions
- tested all extension methods (and they are working)
- updated demo

## v1.0.6
- fixed IsMoving* methods to correctly account for
changes in player position
- added Player for VR support

## v1.0.5
- added extensions methods for IObservable<Hand>
- added extension methods for IObservable<Finger>

## v1.0.4
- refactored stuff for performance gains

## v1.0.3
- fixed hand leaving scene bug
- added Rotate static class to help users with
rotating objects (method names should change)

## v1.0.2
- added CHANGELOG
- extracted interface IReactiveDetector for extendability
- extracted base class ReactiveDetectorBase for extendability
- added DisposedBy extension method using CompositeDisposable
- added HandsDetector - now you can do stuff with both hands!
- BUG!!! - if hand leaves scene, last value is sent down the pipeline