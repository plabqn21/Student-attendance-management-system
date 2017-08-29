namespace Student_attendance_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUser : DbMigration
    {
        public override void Up()
        {

            Sql(@"


                         
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'eed89fea-57e4-4368-8bbe-4342e66c411c', N'Admin')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [DateOfBirth], [Degree], [Position], [SecrectCode], [Mobile], [ProfilePicture]) VALUES (N'2bf110cf-093b-499b-ba75-1b8ff5638153', N'pl@gmail.com', 0, N'AHPc8/CgvOCh2t3L0jU7RoOA3O8N+PTko2FnPsQnZaITyDvn8Ri66YVMDUkQVQgeZQ==', N'6a9119c7-fe8b-46fc-b675-a452e52bf740', NULL, 0, 0, NULL, 1, 0, N'pl@gmail.com', N'Plabon', N'2 aug 1991', N'MSC', N'Lecturer', N'xyz122', N'01913099009', N'gggggg')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [DateOfBirth], [Degree], [Position], [SecrectCode], [Mobile], [ProfilePicture]) VALUES (N'3b709b9d-38f0-4ad5-afd7-a299cbf3d99d', N'plabon@gmail.com', 0, N'APWBjsrOIyg2Y6xXXalRsECGHIQHHKGxcG4CuJ3j2y3+w4wlfj8njAJjU3sFKSSLtA==', N'ce693829-d959-4971-b084-cbf5d08388a6', NULL, 0, 0, NULL, 1, 0, N'plabon@gmail.com', N'Plabon', N'2 aug 1991', N'MSC', N'Lecturer', N'abcd', N'01913099009', N'gggggg')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [DateOfBirth], [Degree], [Position], [SecrectCode], [Mobile], [ProfilePicture]) VALUES (N'bd44a3b6-117f-412e-ad15-3e7e589a0c2b', N'admin@csejnu.com', 0, N'AAQDLq+FBctJLvOlIa2pxhvBdP/i3PxTvxPxzTluVj+uah3+9cCR8zYSglHmFj5nVw==', N'951bf23d-922a-4707-848e-c89ceda82bb6', NULL, 0, 0, NULL, 1, 0, N'admin@csejnu.com', N'Admin', N'xxxx', N'PhD', N'Chairman', N'abcd', N'01671082885', N'ProfilePicture')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [DateOfBirth], [Degree], [Position], [SecrectCode], [Mobile], [ProfilePicture]) VALUES (N'e1040b2f-41dc-4c9c-9efc-420b0fb487e1', N'plabon.pm@gmail.com', 0, N'ABKTN1sApEIbXYNtPoxXCnFdHxohjB5GxxvRE04pnhc8udx7qHINo/4NPpZEI8w3NA==', N'f72605f5-4ce3-467c-a79b-7ac5c2306d5b', NULL, 0, 0, NULL, 1, 0, N'plabon.pm@gmail.com', N'Plabon', N'2 aug 1991', N'MSC', N'Lecturer', N'fffffff', N'01919988998', N'hhhhh')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bd44a3b6-117f-412e-ad15-3e7e589a0c2b', N'eed89fea-57e4-4368-8bbe-4342e66c411c')
GO



                ");
        }
        
        public override void Down()
        {
        }
    }
}
