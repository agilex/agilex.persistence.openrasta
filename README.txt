== agilex.persistence.openrasta

Uses an OR pipeline to inject an agilex.persistence.IRepository instance into your context 
Also disposes of repository when closing pipeline (causing a commit)

To configure call AgilexOpenRasta.Configure() and provide your database configuration as
an agilex.persistence.IDatabaseConfiguration (concrete implementation available - agilex.persistence.DatabaseConfiguration)