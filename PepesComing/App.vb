Imports Microsoft.Xna.Framework


Public Class Application
    Shared _game As Game

    Shared Sub Main()
        Try
            _game = New Game1()
            _game.Run()
        Catch ex As Exception
            Console.WriteLine("Boo! something went wrong!")
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.InnerException)
            Console.WriteLine(ex.StackTrace)
            Console.ReadLine()
        End Try

    End Sub
End Class