
TicketAppContext Migration

Add-Migration <MigrationName> -c TicketAppContext  -o "TaskAssignmentApp.Persistance/ORM/EntityFramework/Migrations"
Update-Database -c TicketAppContext


IdentityContext Migration

Add-Migration <MigrationName> -c IdentityContext  -o "TaskAssignmentApp.Persistance/ORM/EntityFramework/Migrations/IdentityDb"
Update-Database -c IdentityContext