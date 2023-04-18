
TicketAppContext Migration

Add-Migration <MigrationName> -context TicketAppContext  -o "ORM/EntityFramework/Migrations/TicketAppDb"
Update-Database -context TicketAppContext


IdentityContext Migration

Add-Migration <MigrationName> -c IdentityContext  -o "TaskAssignmentApp.Persistance/ORM/EntityFramework/Migrations/IdentityDb"
Update-Database -c IdentityContext