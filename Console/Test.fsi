module M

type Test =
    new : bool -> Test      // <-- here is what's causing it
    member Status: bool