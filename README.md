<h1>ADO.NET Data Access Layer Code Generator</h1>
<p>A C# windows forms application that read local machine's server's metadata in order to offer the service of  generating ADO.NET based code for data access layer.</p>
<p>
it assume that user have already created the stored procedures he need to build DAL based on them. 
<p>
<hr>
<section>
<h2>Working Mechanism</h2>
<ol>
<li>auto detects the internal server</li>
<li>auto detects the databases in server</li>
<li>allows user to choose between local databases</li>
<li>auto reads to stored procedures meta data from the choosen database</li>
<li>creates DTOs based on the create's stored procedure of each table and allows user to choose between them</li>
<li>Assigns each stored procedure to its corresponding DTO</li>
<li>tries to classify stored procedures based on two factors, entity name (User, Person, License, etc...), and operation type (Create, Read, Update, Delete)</li>
<li>in order to have correct classification, it assume that user has already defined stored procedures names like the following pattern "sp_OperationType_EntityName(singular)"</li>
</ol>
</section>
<hr>
<section>
  <h2>Visual Overview:</h2>
  <img src="https://raw.githubusercontent.com/Saeed-AbuYasser/Data-Access-Layer-Code-Generator/refs/heads/main/Assets/Code%20Generator%20video.gif">
  <br>
</section>
<hr>
<section>
  <h2>How to Design Stored Procedures to Get Best Results</h2>
  <pre>
    
--------------------------------------------------------------

CREATE PROCEDURE sp_Create_{Entity Name}
{Parameters (With Exactly one OUTPUT Parameter as the PK, if there were more or less than one; it's going to throw an exception)} 
AS
	BEGIN
		{Creation Logic (You're expected to set a value for the PK 
		,which is the OUTPUT Parameter, inside the SP, or to set identity on it.
		make sure if the creation logic faild you well empty the PK of its value if you had assigned one).}
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_ReadAll_{Entity Name}
AS
	BEGIN
		SELECT {Columns you need to select} FROM {Table} {You can JOIN if you want to};
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_ReadByID_{Entity Name}
@ID UNIQUEIDENTIFIER
AS
	BEGIN
		SELECT {Columns you need to select} FROM {Table} WHERE ID = @ID
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_ReadByNationalNo_{Entity Name}
{Exactly one UNIQUE parameter, if it's not UNIQUE; you well get unexpected behavior.}
AS
	BEGIN
		SELECT {Columns you need to select} FROM {Table} WHERE {TableParameterName = @ParameterName}

  END

--------------------------------------------------------------

CREATE PROCEDURE sp_ExistsByID_{Entity Name}
@ID UNIQUEIDENTIFIER
AS
	BEGIN
		SELECT CASE
			WHEN (SELECT ID FROM {Table} WHERE ID = @ID) is not null THEN 1
			ELSE 0
		END
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_ExistsByNationalNo_{Entity Name}
{Exactly one UNIQUE parameter, if it's not UNIQUE; you well get unexpected behavior.}
AS
	BEGIN
		SELECT CASE
			WHEN (SELECT ID FROM {Table} WHERE {TableParameterName = @ParameterName}) is not null THEN 1
			ELSE 0
		END
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_Update_{Entity Name}
{Parameters}
AS

  BEGIN
		{Updating Logic (make sure you return 1 
		(or any value bigger than 0) 
		if the updating operation is successful)}
	END

--------------------------------------------------------------

CREATE PROCEDURE sp_Delete_{Entity Name}
{Parameters}
AS
	BEGIN
		Delete FROM {Table} WHERE {TableParameterName = @ParameterName}
	END
  </pre>
</section>
<hr>
<section>
<h2>Next Version Expectations</h2>
<ul>
<li>basic CRUD Operation's Stored procedures code generator based on clean defined tables</li>
</ul>
</section>
