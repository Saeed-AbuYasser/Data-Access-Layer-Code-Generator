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
<li>tries to classify stored procedures based on two factors, entity name (User, Person, License, etc...), and operation type (Create, Read, Update, Delete)</li>
<li>in order to have correct classification, it assume that user has already defined stored procedures names like the following pattern "sp_OperationType_EntityName(singular)"</li>
</ol>
<hr>
</section>
<h2>Next Version Expectations</h2>
<ul>
<li>basic CRUD Operation's Stored procedures code generator based on clean defined tables</li>
</ul>
<section>