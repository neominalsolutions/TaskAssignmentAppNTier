
TicketAppContext Migration

Add-Migration <MigrationName> -context TicketAppContext  -o "ORM/EntityFramework/Migrations/TicketAppDb"
Update-Database -context TicketAppContext


IdentityContext Migration

Add-Migration <MigrationName> -context IdentityContext  -o "ORM/EntityFramework/Migrations/IdentityDb"
Update-Database -context IdentityContext