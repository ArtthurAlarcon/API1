Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports API1

Namespace Controllers
    Public Class UsuariosController
        Inherits System.Web.Http.ApiController

        Private db As New IDGS01_Api1Entities

        ' GET: api/Usuarios
        Function GetDbUsuario() As IQueryable(Of DbUsuario)
            Return db.DbUsuario
        End Function

        ' GET: api/Usuarios/5
        <ResponseType(GetType(DbUsuario))>
        Function GetDbUsuario(ByVal id As Integer) As IHttpActionResult
            Dim dbUsuario As DbUsuario = db.DbUsuario.Find(id)
            If IsNothing(dbUsuario) Then
                Return NotFound()
            End If

            Return Ok(dbUsuario)
        End Function

        ' PUT: api/Usuarios/5
        <ResponseType(GetType(Void))>
        Function PutDbUsuario(ByVal id As Integer, ByVal dbUsuario As DbUsuario) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = dbUsuario.Id Then
                Return BadRequest()
            End If

            db.Entry(dbUsuario).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (DbUsuarioExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Usuarios
        <ResponseType(GetType(DbUsuario))>
        Function PostDbUsuario(ByVal dbUsuario As DbUsuario) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.DbUsuario.Add(dbUsuario)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = dbUsuario.Id}, dbUsuario)
        End Function

        ' DELETE: api/Usuarios/5
        <ResponseType(GetType(DbUsuario))>
        Function DeleteDbUsuario(ByVal id As Integer) As IHttpActionResult
            Dim dbUsuario As DbUsuario = db.DbUsuario.Find(id)
            If IsNothing(dbUsuario) Then
                Return NotFound()
            End If

            db.DbUsuario.Remove(dbUsuario)
            db.SaveChanges()

            Return Ok(dbUsuario)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function DbUsuarioExists(ByVal id As Integer) As Boolean
            Return db.DbUsuario.Count(Function(e) e.Id = id) > 0
        End Function
    End Class
End Namespace