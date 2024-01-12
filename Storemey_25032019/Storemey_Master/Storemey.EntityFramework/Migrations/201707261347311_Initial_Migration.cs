namespace Storemey.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;

    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpAuditLogs",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(),
                    ServiceName = c.String(maxLength: 256),
                    MethodName = c.String(maxLength: 256),
                    Parameters = c.String(maxLength: 1024),
                    ExecutionTime = c.DateTime(nullable: false),
                    ExecutionDuration = c.Int(nullable: false),
                    ClientIpAddress = c.String(maxLength: 64),
                    ClientName = c.String(maxLength: 128),
                    BrowserInfo = c.String(maxLength: 256),
                    Exception = c.String(maxLength: 2000),
                    ImpersonatorUserId = c.Long(),
                    ImpersonatorTenantId = c.Int(),
                    CustomData = c.String(maxLength: 2000),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpBackgroundJobs",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    JobType = c.String(nullable: false, maxLength: 512),
                    JobArgs = c.String(nullable: false),
                    TryCount = c.Short(nullable: false),
                    NextTryTime = c.DateTime(nullable: false),
                    LastTryTime = c.DateTime(),
                    IsAbandoned = c.Boolean(nullable: false),
                    Priority = c.Byte(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.IsAbandoned, t.NextTryTime });

            CreateTable(
                "dbo.AbpFeatures",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    Value = c.String(nullable: false, maxLength: 2000),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                    EditionId = c.Int(),
                    TenantId = c.Int(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantFeatureSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);

            CreateTable(
                "dbo.AbpEditions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 32),
                    DisplayName = c.String(nullable: false, maxLength: 64),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpLanguages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenantId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 10),
                    DisplayName = c.String(nullable: false, maxLength: 64),
                    Icon = c.String(maxLength: 128),
                    IsDisabled = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpLanguageTexts",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    LanguageName = c.String(nullable: false, maxLength: 10),
                    Source = c.String(nullable: false, maxLength: 128),
                    Key = c.String(nullable: false, maxLength: 256),
                    Value = c.String(nullable: false),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpNotifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    NotificationName = c.String(nullable: false, maxLength: 96),
                    Data = c.String(),
                    DataTypeName = c.String(maxLength: 512),
                    EntityTypeName = c.String(maxLength: 250),
                    EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                    EntityId = c.String(maxLength: 96),
                    Severity = c.Byte(nullable: false),
                    UserIds = c.String(),
                    ExcludedUserIds = c.String(),
                    TenantIds = c.String(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpNotificationSubscriptions",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    NotificationName = c.String(maxLength: 96),
                    EntityTypeName = c.String(maxLength: 250),
                    EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                    EntityId = c.String(maxLength: 96),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.NotificationName, t.EntityTypeName, t.EntityId, t.UserId });

            CreateTable(
                "dbo.AbpOrganizationUnits",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    ParentId = c.Long(),
                    Code = c.String(nullable: false, maxLength: 95),
                    DisplayName = c.String(nullable: false, maxLength: 128),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ParentId)
                .Index(t => t.ParentId);

            CreateTable(
                "dbo.AbpPermissions",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 128),
                    IsGranted = c.Boolean(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                    RoleId = c.Int(),
                    UserId = c.Long(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            //.ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
            //.ForeignKey("dbo.AbpRoles", t => t.RoleId, cascadeDelete: true)
            //.Index(t => t.RoleId)
            //.Index(t => t.UserId);

            CreateTable(
                "dbo.AbpRoles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                    TenantId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 32),
                    DisplayName = c.String(nullable: false, maxLength: 64),
                    IsStatic = c.Boolean(nullable: false),
                    IsDefault = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                //.ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                //.ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);

            CreateTable(
                "dbo.AbpUsers",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AuthenticationSource = c.String(maxLength: 64),
                    UserName = c.String(nullable: false, maxLength: 32),
                    TenantId = c.Int(),
                    EmailAddress = c.String(nullable: false, maxLength: 256),
                    Name = c.String(nullable: false, maxLength: 32),
                    Surname = c.String(nullable: false, maxLength: 32),
                    Password = c.String(nullable: false, maxLength: 128),
                    EmailConfirmationCode = c.String(maxLength: 328),
                    PasswordResetCode = c.String(maxLength: 328),
                    LockoutEndDateUtc = c.DateTime(),
                    AccessFailedCount = c.Int(nullable: false),
                    IsLockoutEnabled = c.Boolean(nullable: false),
                    PhoneNumber = c.String(),
                    IsPhoneNumberConfirmed = c.Boolean(nullable: false),
                    SecurityStamp = c.String(),
                    IsTwoFactorEnabled = c.Boolean(nullable: false),
                    IsEmailConfirmed = c.Boolean(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    LastLoginTime = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);

            CreateTable(
                "dbo.AbpUserClaims",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AbpUserLogins",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 256),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    RoleId = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AbpSettings",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(),
                    Name = c.String(nullable: false, maxLength: 256),
                    Value = c.String(maxLength: 2000),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AbpTenantNotifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TenantId = c.Int(),
                    NotificationName = c.String(nullable: false, maxLength: 96),
                    Data = c.String(),
                    DataTypeName = c.String(maxLength: 512),
                    EntityTypeName = c.String(maxLength: 250),
                    EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                    EntityId = c.String(maxLength: 96),
                    Severity = c.Byte(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpTenants",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EditionId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 128),
                    TenancyName = c.String(nullable: false, maxLength: 64),
                    ConnectionString = c.String(maxLength: 1024),
                    IsActive = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.EditionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);

            CreateTable(
                "dbo.AbpUserAccounts",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    UserLinkId = c.Long(),
                    UserName = c.String(),
                    EmailAddress = c.String(),
                    LastLoginTime = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeleterUserId = c.Long(),
                    DeletionTime = c.DateTime(),
                    LastModificationTime = c.DateTime(),
                    LastModifierUserId = c.Long(),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AbpUserLoginAttempts",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    TenancyName = c.String(maxLength: 64),
                    UserId = c.Long(),
                    UserNameOrEmailAddress = c.String(maxLength: 255),
                    ClientIpAddress = c.String(maxLength: 64),
                    ClientName = c.String(maxLength: 128),
                    BrowserInfo = c.String(maxLength: 256),
                    Result = c.Byte(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.UserId, t.TenantId })
                .Index(t => new { t.TenancyName, t.UserNameOrEmailAddress, t.Result });

            CreateTable(
                "dbo.AbpUserNotifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    TenantNotificationId = c.Guid(nullable: false),
                    State = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.UserId, t.State, t.CreationTime });

            CreateTable(
                "dbo.AbpUserOrganizationUnits",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TenantId = c.Int(),
                    UserId = c.Long(nullable: false),
                    OrganizationUnitId = c.Long(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    CreatorUserId = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserOrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreCurrencies",
          c => new
          {
              Id = c.Long(nullable: false, identity: true),
              Currency = c.String(),
              Symbol = c.String(),
              Digital_code = c.String(),
              Name = c.String(),
              Country = c.String(),
              /// Comman Use columns
              IsActive = c.Boolean(nullable: false, defaultValue: true),
              IsDeleted = c.Boolean(nullable: false, defaultValue: false),
              DeleterUserId = c.Long(defaultValue: 0),
              DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
              LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
              LastModifierUserId = c.Long(defaultValue: 0),
              CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
              CreatorUserId = c.Long(defaultValue: 0),
              Note = c.String(),
          }
          ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreTimeZones",
     c => new
     {
         Id = c.Long(nullable: false, identity: true),
         name = c.String(true, 50000),
         current_utc_offset = c.String(true, 200),
         is_currently_dst = c.Int(),
         IsSelected = c.Boolean(defaultValue: false),

         IsActive = c.Boolean(nullable: false, defaultValue: true),
         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
         DeleterUserId = c.Long(defaultValue: 0),
         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
         LastModifierUserId = c.Long(defaultValue: 0),
         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
         CreatorUserId = c.Long(defaultValue: 0),
         Note = c.String(),
     }
     ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreGeneralSettings",
            c => new
            {
                Id = c.Long(nullable: false, identity: true),

                TotalOutlets = c.Int(),
                TotalResiters = c.Int(),
                TotalUsers = c.Int(),
                
                MasterRecordEntry = c.Boolean(),
                MaxFileStorage = c.Int(),

                CurrancyId = c.Long(),
                TimeZoneId = c.Long(),
                TimeZone = c.String(true,500),
                DisplayPrice = c.String(true,500),
                PrivateURL = c.String(true, 500),
                LablePrinterFormat = c.String(true, 500),
                TradingName = c.String(true, 500),
                FacebookUrl = c.String(true, 500),
                LinkedinUrl = c.String(true, 500),
                TwitterUrl = c.String(true, 500),
                WebsiteUrl = c.String(true, 500),
                StoreLogoSize1 = c.String(true, 500),
                StoreLogoSize2 = c.String(true, 500),
                StoreLogoSize3 = c.String(true, 500),


                IsActive = c.Boolean(nullable: false, defaultValue: true),
                IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                DeleterUserId = c.Long(defaultValue: 0),
                DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModifierUserId = c.Long(defaultValue: 0),
                CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                CreatorUserId = c.Long(defaultValue: 0),
            }
        ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreUsers",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    FirstName = c.String(true, 500),
                    LastName = c.String(true, 500),
                    Image = c.String(),
                    EmailAddress = c.String(true, 500),
                    PhoneNumber = c.String(true, 500),
                    UserName = c.String(true, 500),
                    Password = c.String(true, 500),
                    NormalPassword = c.String(true, 500),
                    Note = c.String(),
                    IsActive = c.Boolean(nullable: false, defaultValue: true),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    DeleterUserId = c.Long(defaultValue: 0),
                    DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                    LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                    LastModifierUserId = c.Long(defaultValue: 0),
                    CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorUserId = c.Long(defaultValue: 0),
                    ABPUserId = c.Long(),
                }
            ).PrimaryKey(t => t.Id);





            CreateTable("dbo.StoreCategories",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(true, 200),
                        Image = c.String(),


                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
             ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreBrands",
                          c => new
                          {
                              Id = c.Long(nullable: false, identity: true),
                              BrandName = c.String(true, 200),
                              Description = c.String(true, 200),

                              /// Comman Use columns
                              IsActive = c.Boolean(nullable: false, defaultValue: true),
                              IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                              DeleterUserId = c.Long(defaultValue: 0),
                              DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                              LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                              LastModifierUserId = c.Long(defaultValue: 0),
                              CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                              CreatorUserId = c.Long(defaultValue: 0),
                              Note = c.String(),
                          }
                  ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreCityMaster",
                       c => new
                       {
                           Id = c.Long(nullable: false, identity: true),
                           CountryId = c.Long(defaultValue: 0),
                           StateId = c.Long(),
                           CityName = c.String(true, 200),
                           Zipcode = c.String(true, 200, defaultValue: "0"),

                           /// Comman Use columns
                           IsActive = c.Boolean(nullable: false, defaultValue: true),
                           IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                           DeleterUserId = c.Long(defaultValue: 0),
                           DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                           LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                           LastModifierUserId = c.Long(defaultValue: 0),
                           CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                           CreatorUserId = c.Long(defaultValue: 0),
                           Note = c.String(),
                       }
               ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreCountryMaster",
                 c => new
                 {
                     Id = c.Long(nullable: false, identity: true),
                     CountryName = c.String(true, 200),
                     CountryCode = c.String(true, 200),

                     /// Comman Use columns
                     IsActive = c.Boolean(nullable: false, defaultValue: false),
                     IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                     DeleterUserId = c.Long(defaultValue: 0),
                     DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                     LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                     LastModifierUserId = c.Long(defaultValue: 0),
                     CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                     CreatorUserId = c.Long(defaultValue: 0),
                     Note = c.String(),
                 }
            ).PrimaryKey(t => t.Id);





            CreateTable("dbo.StoreCustomers",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),
                         CustomerCode = c.String(true, 200),
                         FirstName = c.String(true, 200),
                         LastName = c.String(true, 200),
                         Gender = c.String(true, 200),
                         DateOfBirth = c.DateTime(),
                         CompanyName = c.String(true, 200),

                         Bill_Email = c.String(true, 200),
                         Bill_Phone = c.String(true, 200),
                         Bill_Address = c.String(),
                         Bill_Country = c.String(true, 200),
                         Bill_State = c.String(true, 200),
                         Bill_City = c.String(true, 200),
                         Bill_ZipCode = c.String(true, 200),
                         Creaditlimit = c.Int(),
                         Ship_FirstName = c.String(true, 200),
                         Ship_LastName = c.String(true, 200),
                         Ship_Address = c.String(),
                         Ship_Country = c.String(true, 200),
                         Ship_State = c.String(true, 200),
                         Ship_City = c.String(true, 200),
                         Ship_Zipcode = c.String(true, 200),
                         Ship_Phone = c.String(true, 200),
                         Ship_Email = c.String(true, 200),


                         IsActive = c.Boolean(nullable: false, defaultValue: true),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StorePaymentTypes",
                          c => new
                          {
                              Id = c.Long(nullable: false, identity: true),
                              Name = c.String(true, 200),
                              MerchantId = c.Int(),
                              TerminalId = c.Int(),

                              /// Comman Use columns
                              IsActive = c.Boolean(nullable: false, defaultValue: true),
                              IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                              DeleterUserId = c.Long(defaultValue: 0),
                              DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                              LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                              LastModifierUserId = c.Long(defaultValue: 0),
                              CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                              CreatorUserId = c.Long(defaultValue: 0),
                              Note = c.String(),
                          }
                     ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreReceiptTemplates",
                        c => new
                        {
                            Id = c.Long(nullable: false, identity: true),
                            Name = c.String(true, 200),
                            PrintBarcode = c.Boolean(),
                            Logo = c.String(true, 200),
                            HeaderText = c.String(true, 200),
                            InvoiceNoPrefix = c.String(true, 200),
                            InoviceHeading = c.String(true, 200),
                            ServedByLabel = c.String(true, 200),
                            DiscountLabel = c.String(true, 200),
                            SubTotalLabel = c.String(true, 200),
                            TaxLabel = c.String(true, 200),
                            ToPayLabel = c.String(true, 200),
                            TotalLabel = c.String(true, 200),
                            ChangeLable = c.String(true, 200),
                            FooterText = c.String(true, 200),

                            IsActive = c.Boolean(nullable: false, defaultValue: true),
                            IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                            DeleterUserId = c.Long(defaultValue: 0),
                            DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                            LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                            LastModifierUserId = c.Long(defaultValue: 0),
                            CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                            CreatorUserId = c.Long(defaultValue: 0),
                            Note = c.String(),
                        }
                   ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreStateMaster",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),
                         StateName = c.String(true, 200),
                         CountryId = c.Long(),

                         /// Comman Use columns
                         IsActive = c.Boolean(nullable: false, defaultValue: false),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreSuppliers",
               c => new
               {
                   Id = c.Long(nullable: false, identity: true),
                   SupplierFullName = c.String(),
                   Description = c.String(),
                   FirstName = c.String(true, 200),
                   LastName = c.String(true, 200),
                   CompanyName = c.String(true, 200),
                   Email = c.String(true, 200),
                   Phone = c.String(true, 200),
                   Mobile = c.String(true, 200),
                   Fax = c.String(true, 200),
                   Website = c.String(true, 200),
                   Address = c.String(),
                   PostCode = c.String(true, 200),
                   Country = c.String(true, 200),
                   State = c.String(true, 200),
                   City = c.String(true, 200),


                   IsActive = c.Boolean(nullable: false, defaultValue: true),
                   IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                   DeleterUserId = c.Long(defaultValue: 0),
                   DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModifierUserId = c.Long(defaultValue: 0),
                   CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                   CreatorUserId = c.Long(defaultValue: 0),
                   Note = c.String(),
               }
          ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreWarehouses",
               c => new
               {
                   Id = c.Long(nullable: false, identity: true),


                   WarehouseName = c.String(true, 200),
                   Street = c.String(),
                   Street1 = c.String(),
                   Country = c.String(true, 200),
                   CountryId = c.Long(),
                   State = c.String(true, 200),
                   StateId = c.Long(),
                   City = c.String(true, 200),
                   CityId = c.Long(),
                   ZipCode = c.String(true, 200),
                   ContactNumber = c.String(true, 200),
                   MobileNumber = c.String(true, 200),
                   Email = c.String(true, 200),


                   IsActive = c.Boolean(nullable: false, defaultValue: true),
                   IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                   DeleterUserId = c.Long(defaultValue: 0),
                   DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModifierUserId = c.Long(defaultValue: 0),
                   CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                   CreatorUserId = c.Long(defaultValue: 0),
                   Note = c.String(),
               }
          ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreOutlets",
            c => new
            {
                Id = c.Long(nullable: false, identity: true),

                OutletName = c.String(true, 200),
                WarehouseId = c.Long(),
                OrderNumberPrefix = c.String(true, 50),
                OrderNumber = c.String(true, 50),
                IsEnableNagativeInventory = c.Boolean(nullable: false, defaultValue: false),
                SupplierReturnPrefix = c.String(true, 50),
                SupplierReturnNumber = c.String(true, 50),
                Street = c.String(),
                Street1 = c.String(),
                Country = c.String(true, 200),
                CountryId = c.Long(),
                State = c.String(true, 200),
                StateId = c.Long(),
                City = c.String(true, 200),
                CityId = c.Long(),
                ZipCode = c.String(true, 200),
                TimeZone = c.String(true, 200),

                Email = c.String(true, 200),
                ContactNumber = c.String(true, 200),
                MobileNumber = c.String(true, 200),

                IsActive = c.Boolean(nullable: false, defaultValue: true),
                IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                DeleterUserId = c.Long(defaultValue: 0),
                DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModifierUserId = c.Long(defaultValue: 0),
                CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                CreatorUserId = c.Long(defaultValue: 0),
                Note = c.String(),
            }
       ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreRegisters",
            c => new
            {
                Id = c.Long(nullable: false, identity: true),

                RegisterName = c.String(true, 200),
                OutletId = c.Long(),
                ReceiptTemplateId = c.Long(),
                ReceiptNumber = c.String(true, 50),
                ReceiptPrefix = c.String(true, 50),
                ReceiptSuffix = c.String(true, 50),
                SelectUserForNextSale = c.Boolean(nullable: false, defaultValue: false),
                EmailReceipt = c.Boolean(nullable: false, defaultValue: false),
                PrintReceipt = c.Boolean(nullable: false, defaultValue: false),
                AskForNote = c.String(true, 500),
                PrintNoteOnReceipt = c.Boolean(nullable: false, defaultValue: false),
                ShowDiscountOnReceipt = c.Boolean(nullable: false, defaultValue: false),

                IsActive = c.Boolean(nullable: false, defaultValue: true),
                IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                DeleterUserId = c.Long(defaultValue: 0),
                DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                LastModifierUserId = c.Long(defaultValue: 0),
                CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                CreatorUserId = c.Long(defaultValue: 0),
                Note = c.String(),
            }
       ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreTags",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),
                         TagName = c.String(true, 200),
                         Description = c.String(true, 200),

                         /// Comman Use columns
                         IsActive = c.Boolean(nullable: false, defaultValue: true),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreTax",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),
                         TaxName = c.String(true, 200),
                         Rate = c.Int(),
                         IsDefault = c.Boolean(),

                         IsActive = c.Boolean(nullable: false, defaultValue: true),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreTaxGroupMapping",
               c => new
               {
                   Id = c.Long(nullable: false, identity: true),
                   TaxId = c.Int(),
                   TaxGroupId = c.Int(),

                   IsActive = c.Boolean(nullable: false, defaultValue: true),
                   IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                   DeleterUserId = c.Long(defaultValue: 0),
                   DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModifierUserId = c.Long(defaultValue: 0),
                   CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                   CreatorUserId = c.Long(defaultValue: 0),
                   Note = c.String(),
               }
          ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreTaxGroups",
               c => new
               {
                   Id = c.Long(nullable: false, identity: true),
                   TaxGroupName = c.String(true, 200),
                   TaxCommaseparated = c.String(true, 200),

                   IsActive = c.Boolean(nullable: false, defaultValue: true),
                   IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                   DeleterUserId = c.Long(defaultValue: 0),
                   DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                   LastModifierUserId = c.Long(defaultValue: 0),
                   CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                   CreatorUserId = c.Long(defaultValue: 0),
                   Note = c.String(),
               }
          ).PrimaryKey(t => t.Id);




            // new tables added from 18/03/2019



            CreateTable("dbo.StoreRoles",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),

                         Name = c.String(true, 200),
                         Heading = c.String(true, 500),
                         Description = c.String(true, 5000),

                         IsActive = c.Boolean(nullable: false, defaultValue: true),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreUserRoleLinks",
                     c => new
                     {
                         Id = c.Long(nullable: false, identity: true),

                         RoleId = c.Long(nullable: true, defaultValue: 0),
                         UserId = c.Long(nullable: true, defaultValue: 0),
                         IsAccesible = c.Boolean(nullable: false, defaultValue: true),

                         IsActive = c.Boolean(nullable: false, defaultValue: true),
                         IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                         DeleterUserId = c.Long(defaultValue: 0),
                         DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                         LastModifierUserId = c.Long(defaultValue: 0),
                         CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                         CreatorUserId = c.Long(defaultValue: 0),
                         Note = c.String(),
                     }
                ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreGiftCards",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        CustomerId = c.Long(nullable: true, defaultValue: 0),
                        GiftcardNumber = c.String(true, 200),
                        TotalAmount = c.Decimal(nullable: false, defaultValue: 0),
                        CurrentAmount = c.Decimal(nullable: false, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreProducts",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        ProductName = c.String(true, 500),
                        IsVariant = c.Boolean(defaultValue: false),
                        SKU = c.String(true, 500),
                        Barcode = c.String(true, 500),
                        SupplierCode = c.String(true, 500),
                        Description = c.String(),
                        IsInventoryOn = c.Boolean(defaultValue: false),
                        IsPOSProduct = c.Boolean(defaultValue: false),
                        IsEcommarceProduct = c.Boolean(defaultValue: false),
                        SupplierId = c.Long(nullable: true, defaultValue: 0),
                        SupplierPrice = c.Decimal(nullable: false, defaultValue: 0),
                        Price = c.Decimal(nullable: false, defaultValue: 0),
                        MarkUp = c.Decimal(nullable: false, defaultValue: 0),
                        PriceExcludingTax = c.Decimal(nullable: false, defaultValue: 0),
                        TaxId = c.Long(nullable: true, defaultValue: 0),
                        PriceIncludingTax = c.Decimal(nullable: false, defaultValue: 0),
                        TotalPrice = c.Decimal(nullable: false, defaultValue: 0),
                        IsStoremeyProduct = c.Boolean(defaultValue: false),
                        IsAllowtoSellOutofStock = c.Boolean(defaultValue: false),
                        MainId = c.Long(nullable: true, defaultValue: 0),
                        IsEnableSEO = c.Boolean(defaultValue: false),
                        MetaTitle = c.String(true, 500),
                        MetaDescription = c.String(true, 500),
                        MetaKeyword = c.String(true, 500),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);






            CreateTable("dbo.StoreProductImages",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        ProductId = c.Long(nullable: true, defaultValue: 0),
                        Image = c.String(true, 2000),
                        IsDefault = c.Boolean(defaultValue: false),
                        IsVariantProductImage = c.Boolean(defaultValue: false),
                        Size1 = c.String(true, 500),
                        Size2 = c.String(true, 500),
                        Size3 = c.String(true, 500),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);






            CreateTable("dbo.StoreProductBrandLinks",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        ProductId = c.Long(nullable: true, defaultValue: 0),
                        BrandId = c.Long(nullable: true, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreProductTagLinks",
                  c => new
                  {
                      Id = c.Long(nullable: false, identity: true),

                      ProductId = c.Long(nullable: true, defaultValue: 0),
                      TagId = c.Long(nullable: true, defaultValue: 0),

                      IsActive = c.Boolean(nullable: false, defaultValue: true),
                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                      DeleterUserId = c.Long(defaultValue: 0),
                      DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModifierUserId = c.Long(defaultValue: 0),
                      CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                      CreatorUserId = c.Long(defaultValue: 0),
                      Note = c.String(),
                  }
             ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreProductSeasonLinks",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        ProductId = c.Long(nullable: true, defaultValue: 0),
                        SeasonId = c.Long(nullable: true, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreProductCategoryLinks",
                  c => new
                  {
                      Id = c.Long(nullable: false, identity: true),

                      ProductId = c.Long(nullable: true, defaultValue: 0),
                      CategoryId = c.Long(nullable: true, defaultValue: 0),

                      IsActive = c.Boolean(nullable: false, defaultValue: true),
                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                      DeleterUserId = c.Long(defaultValue: 0),
                      DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModifierUserId = c.Long(defaultValue: 0),
                      CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                      CreatorUserId = c.Long(defaultValue: 0),
                      Note = c.String(),
                  }
             ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreSeasons",
              c => new
              {
                  Id = c.Long(nullable: false, identity: true),
                  SeasonName = c.String(maxLength:200),

                  IsActive = c.Boolean(nullable: false, defaultValue: true),
                  IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                  DeleterUserId = c.Long(defaultValue: 0),
                  DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                  LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                  LastModifierUserId = c.Long(defaultValue: 0),
                  CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                  CreatorUserId = c.Long(defaultValue: 0),
                  Note = c.String(),
              }
            ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreInventory",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        ProductId = c.Long(nullable: true, defaultValue: 0),
                        TotalStock = c.Int(defaultValue: 0),
                        CurrentStock = c.Int(defaultValue: 0),
                        OutletId = c.Long(nullable: true, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreInventoryPurchaseOrders",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        SupplierId = c.Long(nullable: true, defaultValue: 0),
                        OutletId = c.Long(nullable: true, defaultValue: 0),
                        ReferenceId = c.String(true, 500),
                        ExpectedDateTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        IsReceived = c.Boolean(defaultValue: false),
                        TotalCost = c.Decimal(nullable: false, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreInventoryPurchaseLinks",
                   c => new
                   {
                       Id = c.Long(nullable: false, identity: true),

                       ProductId = c.Long(nullable: true, defaultValue: 0),
                       InventoryPurchaseId = c.Long(nullable: true, defaultValue: 0),
                       OrderQuantity  = c.Int(),
                       Cost = c.Decimal(nullable: false, defaultValue: 0),
                       TaxId = c.Int(),

                       IsActive = c.Boolean(nullable: false, defaultValue: true),
                       IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                       DeleterUserId = c.Long(defaultValue: 0),
                       DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                       LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                       LastModifierUserId = c.Long(defaultValue: 0),
                       CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                       CreatorUserId = c.Long(defaultValue: 0),
                       Note = c.String(),
                   }
              ).PrimaryKey(t => t.Id);





            CreateTable("dbo.StoreInventoryTransferOrders",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),

                        FromOutletId = c.Long(nullable: true, defaultValue: 0),
                        ToOutletId = c.Long(nullable: true, defaultValue: 0),
                        SupplierId = c.Long(nullable: true, defaultValue: 0),
                        ReferenceId = c.String(true, 500),
                        TransferDateTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        IsReceived = c.Boolean(defaultValue: false),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);


            CreateTable("dbo.StoreInventoryTransferLinks",
                   c => new
                   {
                       Id = c.Long(nullable: false, identity: true),

                       ProductId = c.Long(nullable: true, defaultValue: 0),
                       InventoryTransferId = c.Long(nullable: true, defaultValue: 0),
                       OrderQuantity  = c.Int(),

                       IsActive = c.Boolean(nullable: false, defaultValue: true),
                       IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                       DeleterUserId = c.Long(defaultValue: 0),
                       DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                       LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                       LastModifierUserId = c.Long(defaultValue: 0),
                       CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                       CreatorUserId = c.Long(defaultValue: 0),
                       Note = c.String(),
                   }
              ).PrimaryKey(t => t.Id);





            CreateTable("dbo.StoreCashRegister",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),



                        OutletId = c.Long(nullable: true, defaultValue: 0),
                        RegisterId = c.Long(nullable: true, defaultValue: 0),
                        StartBy = c.Long(nullable: true, defaultValue: 0),
                        CloseBy = c.Long(nullable: true, defaultValue: 0),
                        ReferenceId = c.String(true, 500),
                        StartDateTime = c.DateTime(),
                        EndDateTime = c.DateTime(),
                        CashAmount = c.Decimal(nullable: false, defaultValue: 0),
                        CardAmount = c.Decimal(nullable: false, defaultValue: 0),
                        GiftCardAmount = c.Decimal(nullable: false, defaultValue: 0),
                        TotalAmount = c.Decimal(nullable: false, defaultValue: 0),

                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        DeleterUserId = c.Long(defaultValue: 0),
                        DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                        LastModifierUserId = c.Long(defaultValue: 0),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        CreatorUserId = c.Long(defaultValue: 0),
                        Note = c.String(),
                    }
               ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreCashRegisterInOut",
                  c => new
                  {
                      Id = c.Long(nullable: false, identity: true),

                      Transaction = c.String(true, 500),
                      CashRegisterId = c.Long(nullable: true, defaultValue: 0),
                      UserId = c.Long(nullable: true, defaultValue: 0),
                      Amount = c.Decimal(nullable: false, defaultValue: 0),
                     

                      IsActive = c.Boolean(nullable: false, defaultValue: true),
                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                      DeleterUserId = c.Long(defaultValue: 0),
                      DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModifierUserId = c.Long(defaultValue: 0),
                      CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                      CreatorUserId = c.Long(defaultValue: 0),
                      Note = c.String(),
                  }
             ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreSaleTransactions",
                  c => new
                  {
                      Id = c.Long(nullable: false, identity: true),

                      InvoiceId = c.String(true, 500),
                      CustomerId = c.Long(nullable: true, defaultValue: 0),
                      Status = c.String(true, 500),

                      IsPaid = c.Boolean(nullable: false, defaultValue: false),
                      UserId = c.Long(nullable: true, defaultValue: 0),
                      SaleDatetime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      DiscountAmount = c.Decimal(nullable: false, defaultValue: 0),
                      SubTotalAmount = c.Decimal(nullable: false, defaultValue: 0),
                      TaxAmount = c.Decimal(nullable: false, defaultValue: 0),
                      TotalAmount = c.Decimal(nullable: false, defaultValue: 0),
                      ProductTotalAmount = c.Decimal(nullable: false, defaultValue: 0),
                      IsPOSSale = c.Boolean(nullable: false, defaultValue: false),
                      IsEcommareceSale = c.Boolean(nullable: false, defaultValue: false),


                      IsActive = c.Boolean(nullable: false, defaultValue: true),
                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                      DeleterUserId = c.Long(defaultValue: 0),
                      DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModifierUserId = c.Long(defaultValue: 0),
                      CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                      CreatorUserId = c.Long(defaultValue: 0),
                      Note = c.String(),
                  }
             ).PrimaryKey(t => t.Id);




            CreateTable("dbo.StoreSaleItems",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),


                    SaleId = c.Long(nullable: true, defaultValue: 0),
                    InvoiceId = c.String(true, 500),
                    ProductId = c.Long(nullable: true, defaultValue: 0),
                    GiftcardId = c.Long(nullable: true, defaultValue: 0),
                    Quantity  = c.Int(nullable: false, defaultValue: 0),
                    ItemAmount = c.Decimal(nullable: false, defaultValue: 0),

                    IsActive = c.Boolean(nullable: false, defaultValue: true),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    DeleterUserId = c.Long(defaultValue: 0),
                    DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                    LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                    LastModifierUserId = c.Long(defaultValue: 0),
                    CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorUserId = c.Long(defaultValue: 0),
                    Note = c.String(),
                }
           ).PrimaryKey(t => t.Id);



            CreateTable("dbo.StoreSalePayments",
                  c => new
                  {
                      Id = c.Long(nullable: false, identity: true),

                      SaleId = c.Long(nullable: true, defaultValue: 0),
                      InvoiceId = c.String(true, 500),
                      PaymentTypeId = c.Long(nullable: true, defaultValue: 0),
                      Amount = c.Decimal(nullable: false, defaultValue: 0),
                      Status = c.String(true, 500),
                      PaymentDateTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      InvoiceTotalAmount = c.Decimal(nullable: false, defaultValue: 0),

                      IsActive = c.Boolean(nullable: false, defaultValue: true),
                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                      DeleterUserId = c.Long(defaultValue: 0),
                      DeletionTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModificationTime = c.DateTime(defaultValueSql: "GETUTCDATE()"),
                      LastModifierUserId = c.Long(defaultValue: 0),
                      CreationTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                      CreatorUserId = c.Long(defaultValue: 0),
                      Note = c.String(),
                  }
             ).PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserClaims", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions");
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "UserId", "TenantId" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "EditionId" });
            DropIndex("dbo.AbpSettings", new[] { "UserId" });
            DropIndex("dbo.AbpUserRoles", new[] { "UserId" });
            DropIndex("dbo.AbpUserLogins", new[] { "UserId" });
            DropIndex("dbo.AbpUserClaims", new[] { "UserId" });
            DropIndex("dbo.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpPermissions", new[] { "UserId" });
            DropIndex("dbo.AbpPermissions", new[] { "RoleId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "ParentId" });
            DropIndex("dbo.AbpNotificationSubscriptions", new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });
            DropIndex("dbo.AbpFeatures", new[] { "EditionId" });
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            DropTable("dbo.AbpUserOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserOrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLoginAttempts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserAccounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenantNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSettings",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLogins",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserClaims",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpPermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotificationSubscriptions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotifications");
            DropTable("dbo.AbpLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEditions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpFeatures",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantFeatureSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBackgroundJobs");
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
