USE [nature-net];
GO
CREATE PROCEDURE GetDesignIdeas
    @Username nvarchar(64)
AS 

    SET NOCOUNT ON;
    SELECT dbo.Media.*
    FROM  dbo.Activity INNER JOIN
          dbo.Collection ON dbo.Activity.id = dbo.Collection.activity_id INNER JOIN
          dbo.Media ON dbo.Collection.id = dbo.Media.collection_id INNER JOIN
          dbo.[User] ON dbo.Collection.user_id = @Username
	      
GO
