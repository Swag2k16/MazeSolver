Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Public MustInherit Class Solver

    Public MustOverride Function Solve(ByRef world As World) As List(Of Vector2)

End Class
