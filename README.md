# EHExercise
Setup procedure of the APIs:

There are 6 Visual Studio projects in this solution.  The Project "EvolentHealthAPI" is the main API project.  Add all the six projects to a solution and build it.  Then publish this project "EvolentHealthAPI" to access the APIs.

There are a few settings that need to taken care of:

. In the web.config file of "EvolentHealthAPI" project, change values of following settings
    <add key="RepositoryType" value=""/>
    <add key="JsonDBFile" value=""/>
    <add key="ConnectionString" value=""/>

. I have implemented two repositories SQL and Json.

. For SQL the settings are
    <add key="RepositoryType" value="SQL"/>
    <add key="ConnectionString" value="<Connection String>"/>
        To set up the SQL Database:
          Execute the following files in the specified sequence on a SQL server:
            . "....\EvolentHealthExercise\Src\DB\Organization_Schema.sql"
            . "....\EvolentHealthExercise\Src\DB\Organization_SP.sql"
            . "....\EvolentHealthExercise\Src\DB\Organization_Insert.sql"
          Change the connection string in the web.config file to point to the SQL server

. For Json the settings are
    <add key="RepositoryType" value="Json"/>
    <add key="JsonDBFile" value="<Path the the Json Data file>"/>
        Please copy the following file to any path that can be accessed by the APIs
          "....\EvolentHealthExercise\Src\Lib\Repository\Data\EvolentHealthContacts.json"
        Change the "JsonDBFile" settings value in the web.config to point to this file

. The following file contains postman script to test the APIs
    "....\EvolentHealthExercise\Docs\EvolentHealthAPI.postman_collection.json"

    
