// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//ef new migration
// 1. Create a new migration
// 2. Update the database with the new migration
// 3. Seed the database with test data

using Elyze.Models;
using Microsoft.EntityFrameworkCore;
using TenantService;

namespace Elyze.Data
{
    public partial class ElyzeContext : DbContext
    {
        public IConfiguration _configuration { get; }
        private readonly IHttpContextAccessor _httpContext;
        private readonly ITenantService _tenantService;

        public ElyzeContext(DbContextOptions<ElyzeContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITenantService tenantService) : base(options)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor;
            _tenantService = tenantService;
        }

        public virtual DbSet<Area> Area { get; set; } = null!;
        public virtual DbSet<AreaSDG> AreaSDG { get; set; } = null!;
        public virtual DbSet<SDG> SDG { get; set; } = null!;
        public virtual DbSet<AreaIcons> AreaIcons { get; set; } = null!;
        public virtual DbSet<CampiMa> CampiMa { get; set; } = null!;
        public virtual DbSet<TipologieCampiMicroArea> TipologieCampiMicroArea { get; set; } = null!;
        public virtual DbSet<Gri> Gri { get; set; } = null!;
        public virtual DbSet<Gruppo> Gruppo { get; set; } = null!;
        public virtual DbSet<InserimentiFissi> InserimentiFissi { get; set; } = null!;
        public virtual DbSet<Inserimenti> Inserimenti { get; set; } = null!;
        public virtual DbSet<Lingue> Lingue { get; set; } = null!;
        public virtual DbSet<MicroArea> MicroArea { get; set; } = null!;
        public virtual DbSet<OperatoreDiConversione> OperatoriDiConversione { get; set; } = null!;
        public virtual DbSet<Operazione> Operazione { get; set; } = null!;
        public virtual DbSet<Reparto> Reparto { get; set; } = null!;
        public virtual DbSet<Repository> Repository { get; set; } = null!;
        public virtual DbSet<Sede> Sede { get; set; } = null!;
        public virtual DbSet<Societa> Societa { get; set; } = null!;
        public virtual DbSet<StatoSchede> StatoSchede { get; set; } = null!;
        public virtual DbSet<UnitaMisura> UnitaMisura { get; set; } = null!;
        public virtual DbSet<UserArea> UserArea { get; set; } = null!;
        public virtual DbSet<UserGruppo> UserGruppo { get; set; } = null!;
        public virtual DbSet<UserReparto> UserReparto { get; set; } = null!;
        public virtual DbSet<UserSede> UserSede { get; set; } = null!;
        public virtual DbSet<UserSocieta> UserSocieta { get; set; } = null!;
        public virtual DbSet<UserTypesArea> UserTypesArea { get; set; } = null!;
        public virtual DbSet<Valuta> Valuta { get; set; } = null!;
        public virtual DbSet<TipologiaMail> TipologiaMail { get; set; } = null!;
        public virtual DbSet<TemplateMail> TemplateMail { get; set; } = null!;
        public virtual DbSet<Attachment> Attachment { get; set; } = null!;

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            bool isTesting = _configuration.GetSection("Tenant:Test:IsTesting").Get<bool>();
            if (isTesting)
            {
                string connection = _configuration.GetSection("Tenant:Test:ConnectionTest").Get<string>();
                optionsBuilder.UseSqlServer(connection);
                return;
            }

            bool isOnline = _configuration.GetSection("Tenant:IsOnLine").Get<bool>();
            if (!isOnline)
            {
                string onPremiseConnectionString = _configuration.GetSection("Tenant:OnPremiseConfigurarion:OnPremiseConnectionString").Get<string>();
                optionsBuilder.UseSqlServer(onPremiseConnectionString);
                return;
            }

            string? company = this._httpContext?.HttpContext?.Request.Cookies["Company"];
            if (string.IsNullOrWhiteSpace(company) && isOnline)
            {
                throw new Exception("Invalid Tenant");
            }
            else
            {
                bool isMultiTenant = _configuration.GetSection("Tenant:IsMultiTenant").Get<bool>();
                if (!isMultiTenant)
                {
                    string singleTenantConnection = _configuration.GetSection("Tenant:SingleTenantContext").Get<string>();
                    optionsBuilder.UseSqlServer(singleTenantConnection);
                }
                else
                {
                    TenantResponse tenantResponse = _tenantService.GetTenant(company);
                    if (tenantResponse == null)
                    {
                        throw new TenantService.TenantException("Invalid Response Tenant");
                    }

                    bool isBIExternal = _configuration.GetSection("BI:IsExternal").Get<bool>();
                    if (isBIExternal)
                    {
                        this._httpContext?.HttpContext?.Response.Cookies.Append("BILink", tenantResponse.PowerBiLink);
                    }

                    optionsBuilder.UseSqlServer(tenantResponse.ConnectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Valuta>(entity =>
            {
                entity.ToTable("Valuta");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.Property(e => e.Simbolo).HasMaxLength(255);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.DataCreazione)
                    .HasColumnName("Data_Creazione")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DataSpegnimento)
                    .HasColumnName("Data_Spegnimento")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Stato)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Lingue>()
                .HasData(
                    new Lingue() { Id = 1, Sigla = "IT", Descrizione = "Italiano", CreatedAt = null, UpdatedAt = null, CreatedBy = "SYSTEM", UpdatedBy = "SYSTEM", SiglaEstesa = "it-IT" },
                    new Lingue() { Id = 2, Sigla = "EN", Descrizione = "Inglese", CreatedAt = null, UpdatedAt = null, CreatedBy = "SYSTEM", UpdatedBy = "SYSTEM", SiglaEstesa = "en-US" }
                );

            //get the CustomStyle/Confindustria settings form appsettings.json
            modelBuilder.Entity<AreaIcons>()
                .HasData(
                    new AreaIcons() { Id = 1, Codice = "01", Descrizione = "Acqua", LocalPath = "{0}/acqua.{1}" },
                    new AreaIcons() { Id = 2, Codice = "02", Descrizione = "Emissioni", LocalPath = "{0}/emissioni.{1}" },
                    new AreaIcons() { Id = 3, Codice = "03", Descrizione = "Energia", LocalPath = "{0}/energia.{1}" },
                    new AreaIcons() { Id = 4, Codice = "04", Descrizione = "Finanza", LocalPath = "{0}/finanza.{1}" },
                    new AreaIcons() { Id = 5, Codice = "05", Descrizione = "Innovazione", LocalPath = "{0}/innovazione.{1}" },
                    new AreaIcons() { Id = 6, Codice = "06", Descrizione = "Logistica", LocalPath = "{0}/logistica.{1}" },
                    new AreaIcons() { Id = 7, Codice = "07", Descrizione = "Persone", LocalPath = "{0}/persone.{1}" },
                    new AreaIcons() { Id = 8, Codice = "08", Descrizione = "Rifiuti", LocalPath = "{0}/rifiuti.{1}" },
                    new AreaIcons() { Id = 9, Codice = "09", Descrizione = "Salute", LocalPath = "{0}/salute.{1}" },
                    new AreaIcons() { Id = 10, Codice = "10", Descrizione = "Sito produttivo", LocalPath = "{0}/sito-produttivo.{1}" }
                );

            modelBuilder.Entity<SDG>()
                .HasData(
                    new SDG() { Id = 1, Codice = "01", Descrizione = "No poverty", LocalPath = "01-NoPoverty.png" },
                    new SDG() { Id = 2, Codice = "02", Descrizione = "Zero hunger", LocalPath = "02-ZeroHunger.png" },
                    new SDG() { Id = 3, Codice = "03", Descrizione = "Good health and well being", LocalPath = "03-GoodHealthAndWellBeing.png" },
                    new SDG() { Id = 4, Codice = "04", Descrizione = "Quality education", LocalPath = "04-QualityEducation.png" },
                    new SDG() { Id = 5, Codice = "05", Descrizione = "Gender equality", LocalPath = "05-GenderEquality.png" },
                    new SDG() { Id = 6, Codice = "06", Descrizione = "Clean water and sanitation", LocalPath = "06-CleanWaterAndSanitation.png" },
                    new SDG() { Id = 7, Codice = "07", Descrizione = "Affordable and clean energy", LocalPath = "07-AffordableAndCleanEnergy.png" },
                    new SDG() { Id = 8, Codice = "08", Descrizione = "Decent work and economy growth", LocalPath = "08-DecentWorkAndEconomyGrowth.png" },
                    new SDG() { Id = 9, Codice = "09", Descrizione = "Industry innovation and infrastructure", LocalPath = "09-IndustryInnovationAndInfrastructure.png" },
                    new SDG() { Id = 10, Codice = "10", Descrizione = "Reduce inequalities", LocalPath = "10-ReduceInequalities.png" },
                    new SDG() { Id = 11, Codice = "11", Descrizione = "Sustainable cities and communities", LocalPath = "11-SustainableCitiesAndCommunities.png" },
                    new SDG() { Id = 12, Codice = "12", Descrizione = "Responsible consumption and production", LocalPath = "12-ResponsibleConsumptionAndProduction.png" },
                    new SDG() { Id = 13, Codice = "13", Descrizione = "Climate action", LocalPath = "13-ClimateAction.png" },
                    new SDG() { Id = 14, Codice = "14", Descrizione = "Life below water", LocalPath = "14-LifeBelowWater.png" },
                    new SDG() { Id = 15, Codice = "15", Descrizione = "Life and land", LocalPath = "15-LifeAndLand.png" },
                    new SDG() { Id = 16, Codice = "16", Descrizione = "Peace, justice and strong institution", LocalPath = "16-PeaceJusticheAndStrongInsitution.png" },
                    new SDG() { Id = 17, Codice = "17", Descrizione = "Partnership for the goals", LocalPath = "17-PartnershipForTheGoals.png" }
                );

            modelBuilder.Entity<CampiMa>(entity =>
            {
                entity.ToTable("CampiMa");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Gri>(entity =>
            {
                entity.ToTable("Gri");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Gri>()
                .HasData(
                    new Gri() { Id = 1, CodiceGri = "201-1", Titolo = "GRI 201 Performance economiche 2016" },
                    new Gri() { Id = 2, CodiceGri = "201-2", Titolo = "GRI 201 Performance economiche 2016" },
                    new Gri() { Id = 3, CodiceGri = "201-3", Titolo = "GRI 201 Performance economiche 2016" },
                    new Gri() { Id = 4, CodiceGri = "201-4", Titolo = "GRI 201 Performance economiche 2016" },
                    new Gri() { Id = 5, CodiceGri = "202-2", Titolo = "GRI 202 Presenza sul mercato 2016" },
                    new Gri() { Id = 6, CodiceGri = "202-3", Titolo = "GRI 202 Presenza sul mercato 2016" },
                    new Gri() { Id = 7, CodiceGri = "203-1", Titolo = "GRI 203 Impatti economici indiretti 2016" },
                    new Gri() { Id = 8, CodiceGri = "203-2", Titolo = "GRI 203 Impatti economici indiretti 2016" },
                    new Gri() { Id = 9, CodiceGri = "204-1", Titolo = "GRI 204 Prassi di approvvigionamento 2016" },
                    new Gri() { Id = 10, CodiceGri = "205-1", Titolo = "GRI 205 Anticorruzione 2016" },
                    new Gri() { Id = 11, CodiceGri = "205-2", Titolo = "GRI 205 Anticorruzione 2016" },
                    new Gri() { Id = 12, CodiceGri = "205-3", Titolo = "GRI 205 Anticorruzione 2016" },
                    new Gri() { Id = 13, CodiceGri = "206-1", Titolo = "GRI 206 Comportamento anticoncorrenziale 2016" },
                    new Gri() { Id = 14, CodiceGri = "207-1", Titolo = "GRI 207 Tasse 2019" },
                    new Gri() { Id = 15, CodiceGri = "207-2", Titolo = "GRI 207 Tasse 2019" },
                    new Gri() { Id = 16, CodiceGri = "207-3", Titolo = "GRI 207 Tasse 2019" },
                    new Gri() { Id = 17, CodiceGri = "207-4", Titolo = "GRI 207 Tasse 2019" },
                    new Gri() { Id = 18, CodiceGri = "301-1", Titolo = "GRI 301 Materiali 2016" },
                    new Gri() { Id = 19, CodiceGri = "301-2", Titolo = "GRI 301 Materiali 2016" },
                    new Gri() { Id = 20, CodiceGri = "301-3", Titolo = "GRI 301 Materiali 2016" },
                    new Gri() { Id = 21, CodiceGri = "302-1", Titolo = "GRI 302 Energia 2016" },
                    new Gri() { Id = 22, CodiceGri = "302-2", Titolo = "GRI 302 Energia 2016" },
                    new Gri() { Id = 23, CodiceGri = "302-3", Titolo = "GRI 302 Energia 2016" },
                    new Gri() { Id = 24, CodiceGri = "302-4", Titolo = "GRI 302 Energia 2016" },
                    new Gri() { Id = 25, CodiceGri = "302-5", Titolo = "GRI 302 Energia 2016" },
                    new Gri() { Id = 26, CodiceGri = "303-1", Titolo = "GRI 303 Acqua ed effluenti 2018" },
                    new Gri() { Id = 27, CodiceGri = "303-2", Titolo = "GRI 303 Acqua ed effluenti 2018" },
                    new Gri() { Id = 28, CodiceGri = "303-3", Titolo = "GRI 303 Acqua ed effluenti 2018" },
                    new Gri() { Id = 29, CodiceGri = "303-4", Titolo = "GRI 303 Acqua ed effluenti 2018" },
                    new Gri() { Id = 30, CodiceGri = "303-5", Titolo = "GRI 303 Acqua ed effluenti 2018" },
                    new Gri() { Id = 31, CodiceGri = "304-1", Titolo = "GRI 304 Biodiversità 2016" },
                    new Gri() { Id = 32, CodiceGri = "304-2", Titolo = "GRI 304 Biodiversità 2016" },
                    new Gri() { Id = 33, CodiceGri = "304-3", Titolo = "GRI 304 Biodiversità 2016" },
                    new Gri() { Id = 34, CodiceGri = "304-4", Titolo = "GRI 304 Biodiversità 2016" },
                    new Gri() { Id = 35, CodiceGri = "305-1", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 36, CodiceGri = "305-2", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 37, CodiceGri = "305-3", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 38, CodiceGri = "305-4", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 39, CodiceGri = "305-5", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 40, CodiceGri = "305-6", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 41, CodiceGri = "305-7", Titolo = "GRI 305 Emissioni 2016" },
                    new Gri() { Id = 42, CodiceGri = "306-1", Titolo = "GRI 306 Rifiuti 2020" },
                    new Gri() { Id = 43, CodiceGri = "306-2", Titolo = "GRI 306 Rifiuti 2020" },
                    new Gri() { Id = 44, CodiceGri = "306-3 (2020)", Titolo = "GRI 306 Rifiuti 2020" },
                    new Gri() { Id = 45, CodiceGri = "306-4 (2020)", Titolo = "GRI 306 Rifiuti 2020" },
                    new Gri() { Id = 46, CodiceGri = "306-5 (2020)", Titolo = "GRI 306 Rifiuti 2020" },
                    new Gri() { Id = 47, CodiceGri = "306-3 (2016)", Titolo = "GRI 306 Scarichi idrici e rifiuti 2016 " },
                    new Gri() { Id = 48, CodiceGri = "306-4 (2016)", Titolo = "GRI 306 Scarichi idrici e rifiuti 2016 " },
                    new Gri() { Id = 49, CodiceGri = "306-5 (2016)", Titolo = "GRI 306 Scarichi idrici e rifiuti 2016" },
                    new Gri() { Id = 50, CodiceGri = "308-1", Titolo = "GRI 308 Valutazione ambientale dei fornitori 2016" },
                    new Gri() { Id = 51, CodiceGri = "308-2", Titolo = "GRI 308 Valutazione ambientale dei fornitori 2016" },
                    new Gri() { Id = 52, CodiceGri = "401-1", Titolo = "GRI 401 Occupazione 2016" },
                    new Gri() { Id = 53, CodiceGri = "401-2", Titolo = "GRI 401 Occupazione 2016" },
                    new Gri() { Id = 54, CodiceGri = "401-3", Titolo = "GRI 401 Occupazione 2016" },
                    new Gri() { Id = 55, CodiceGri = "402-1", Titolo = "GRI 402 Gestione del lavoro e delle relazioni sindacali 2016" },
                    new Gri() { Id = 56, CodiceGri = "403-1", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 57, CodiceGri = "403-2", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 58, CodiceGri = "403-3", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 59, CodiceGri = "403-4", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 60, CodiceGri = "403-5", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 61, CodiceGri = "403-6", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 62, CodiceGri = "403-7", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 63, CodiceGri = "403-8", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 64, CodiceGri = "403-9", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 65, CodiceGri = "403-10", Titolo = "GRI 403 Salute e sicurezza sul lavoro 2018" },
                    new Gri() { Id = 66, CodiceGri = "404-1", Titolo = "GRI 404 Formazione e istruzione 2016" },
                    new Gri() { Id = 67, CodiceGri = "404-2", Titolo = "GRI 404 Formazione e istruzione 2016" },
                    new Gri() { Id = 68, CodiceGri = "404-3", Titolo = "GRI 404 Formazione e istruzione 2016" },
                    new Gri() { Id = 69, CodiceGri = "405-1", Titolo = "GRI 405 Diversità e pari opportunità 2016" },
                    new Gri() { Id = 70, CodiceGri = "405-2", Titolo = "GRI 405 Diversità e pari opportunità 2016" },
                    new Gri() { Id = 71, CodiceGri = "406-1", Titolo = "GRI 406 Non discriminazione 2016" },
                    new Gri() { Id = 72, CodiceGri = "407-1", Titolo = "GRI 407 Libertà di associazione e contrattazione collettiva 2016" },
                    new Gri() { Id = 73, CodiceGri = "408-1", Titolo = "GRI 408 Lavoro minorile 2016" },
                    new Gri() { Id = 74, CodiceGri = "409-1", Titolo = "GRI 409 Lavoro forzato o obbligatorio 2016" },
                    new Gri() { Id = 75, CodiceGri = "410-1", Titolo = "GRI 410 Pratiche per la sicurezza 2016" },
                    new Gri() { Id = 76, CodiceGri = "411-1", Titolo = "GRI 411 Diritti dei popoli indigeni 2016" },
                    new Gri() { Id = 77, CodiceGri = "413-1", Titolo = "GRI 413 Comunità locali 2016" },
                    new Gri() { Id = 78, CodiceGri = "413-2", Titolo = "GRI 413 Comunità locali 2016" },
                    new Gri() { Id = 79, CodiceGri = "414-1", Titolo = "GRI 414 Valutazione sociale dei fornitori" },
                    new Gri() { Id = 80, CodiceGri = "414-2", Titolo = "GRI 414 Valutazione sociale dei fornitori" },
                    new Gri() { Id = 81, CodiceGri = "415-1", Titolo = "GRI 415 Politica pubblica 2016" },
                    new Gri() { Id = 82, CodiceGri = "416-1", Titolo = "GRI 416 Salute e sicurezza dei clienti 2016" },
                    new Gri() { Id = 83, CodiceGri = "416-2", Titolo = "GRI 416 Salute e sicurezza dei clienti 2016" },
                    new Gri() { Id = 84, CodiceGri = "417-1", Titolo = "GRI 417 Marketing ed etichettatura 2016" },
                    new Gri() { Id = 85, CodiceGri = "417-2", Titolo = "GRI 417 Marketing ed etichettatura 2016" },
                    new Gri() { Id = 86, CodiceGri = "417-3", Titolo = "GRI 417 Marketing ed etichettatura 2016" },
                    new Gri() { Id = 87, CodiceGri = "418-1", Titolo = "GRI 418 Privacy dei clienti 2016" }
                );

            modelBuilder.Entity<Gruppo>(entity =>
            {
                entity.ToTable("Gruppo");

                entity.Property(e => e.Attivo)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Inserimenti>(entity =>
            {
                entity.ToTable("Inserimenti");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Lingue>(entity =>
            {
                entity.ToTable("Lingue");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<MicroArea>(entity =>
            {
                entity.ToTable("MicroArea");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.DataCreazione)
                    .HasColumnName("Data_Creazione")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DataSpegnimento)
                    .HasColumnName("Data_Spegnimento")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.IdGri).HasColumnName("Id_Gri");

                entity.Property(e => e.NomeTabella).HasColumnName("Nome_Tabella");

                entity.Property(e => e.Stato)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

            });

            modelBuilder.Entity<OperatoreDiConversione>(entity =>
            {
                entity.ToTable("OperatoreDiConversione");

                entity.HasIndex(e => e.IdOperazione, "IX_OperatoreDiConversione_IdOperazione");

                entity.HasIndex(e => e.IdUnitaDiMisuraArrivo, "IX_OperatoreDiConversione_IdUnitaDiMisuraArrivo");

                entity.HasIndex(e => e.IdUnitaDiMisuraPartenza, "IX_OperatoreDiConversione_IdUnitaDiMisuraPartenza");

                entity.Property(e => e.FattoreDiConversione).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.IdOperazioneNavigation)
                    .WithMany(p => p.OperatoreDiConversioneIdOperazione)
                    .HasForeignKey(d => d.IdOperazione)
                    .HasConstraintName("FK_OperatoreDiConversione_UnitaMisura_Operazione");

                entity.HasOne(d => d.IdUnitaDiMisuraArrivoNavigation)
                    .WithMany(p => p.OperatoreDiConversioneIdUnitaDiMisuraArrivoNavigations)
                    .HasForeignKey(d => d.IdUnitaDiMisuraArrivo)
                    .HasConstraintName("FK_OperatoreDiConversione_UnitaMisura_Arrivo");

                entity.HasOne(d => d.IdUnitaDiMisuraPartenzaNavigation)
                    .WithMany(p => p.OperatoreDiConversioneIdUnitaDiMisuraPartenzaNavigations)
                    .HasForeignKey(d => d.IdUnitaDiMisuraPartenza)
                    .HasConstraintName("FK_OperatoreDiConversione_UnitaMisura_Partenza");
            });

            modelBuilder.Entity<Operazione>(entity =>
            {
                entity.ToTable("Operazione");

                entity.Property(e => e.OperatoreDiConversione).HasMaxLength(50);
            });

            modelBuilder.Entity<Operazione>().HasData(
                new Operazione() { Id = 1, OperatoreDiConversione = "+" },
                new Operazione() { Id = 2, OperatoreDiConversione = "-" },
                new Operazione() { Id = 3, OperatoreDiConversione = "/" },
                new Operazione() { Id = 4, OperatoreDiConversione = "*" });

            modelBuilder.Entity<TipologieCampiMicroArea>().HasData(
                new TipologieCampiMicroArea() { Id = 1, Nome = "Decimal" },
                new TipologieCampiMicroArea() { Id = 2, Nome = "Integer" },
                new TipologieCampiMicroArea() { Id = 3, Nome = "Text" });

            modelBuilder.Entity<UserTypesArea>().HasData(
                new UserTypesArea() { Id = 1, Name = "Validator" },
                new UserTypesArea() { Id = 2, Name = "Compiler" });

            modelBuilder.Entity<Reparto>(entity =>
            {
                entity.ToTable("Reparto");

                entity.Property(e => e.Attivo)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            #region Repository
            modelBuilder.Entity<Repository>(entity =>
            {
                entity.ToTable("Repository");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Repository>()
                .HasOne(x => x.Societa)
                .WithMany(x => x.Repository)
                .HasForeignKey(x => x.SocietaId);

            modelBuilder.Entity<Repository>()
                .HasOne(x => x.Attachment)
                .WithMany(x => x.Repository)
                .HasForeignKey(x => x.AttachmentId);
            #endregion

            modelBuilder.Entity<Sede>(entity =>
            {
                entity.ToTable("Sede");

                entity.Property(e => e.Attivo)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<Societa>(entity =>
            {
                entity.Property(e => e.Attivo)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<StatoSchede>(entity =>
            {
                entity.ToTable("StatoSchede");
            });

            modelBuilder.Entity<UnitaMisura>(entity =>
            {
                entity.ToTable("UnitaMisura");
            });

            modelBuilder.Entity<UserArea>(entity =>
            {
                entity.ToTable("UserArea");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<TipologiaMail>()
                .HasData(new TipologiaMail() { Id = 1, Descrizione = "ResetPassword" });

            modelBuilder.Entity<TemplateMail>()
                .HasOne(x => x.TipologiaMail)
                .WithMany(x => x.TemplateMails)
                .HasForeignKey(x => x.TipoMailId);

            #region Attachment
            modelBuilder.Entity<Attachment>()
                .Property(x => x.Description)
                .HasMaxLength(255);

            modelBuilder.Entity<Attachment>()
                .Property(x => x.FileName)
                .HasMaxLength(255);

            modelBuilder.Entity<Attachment>()
                .Property(x => x.FileExtension)
                .HasMaxLength(255);

            modelBuilder.Entity<Attachment>()
                .Property(x => x.Description)
                .HasMaxLength(255);
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entityBase = (EntityBase)entityEntry.Entity;
                entityBase.UpdatedAt = DateTime.Now;
                entityBase.UpdatedBy = "UTENTE";

                //var name = m_oHttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                //((EntityBase)entityEntry.Entity).UpdatedBy = name; TODO da mettere l'utente loggato

                if (entityEntry.State == EntityState.Added)
                {
                    entityBase.CreatedAt = DateTime.Now;
                    entityBase.CreatedBy = "UTENTE";
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entityBase = (EntityBase)entityEntry.Entity;
                entityBase.UpdatedAt = DateTime.Now;
                entityBase.UpdatedBy = "UTENTE";

                //var name = m_oHttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                //((EntityBase)entityEntry.Entity).UpdatedBy = name; TODO da mettere l'utente loggato

                if (entityEntry.State == EntityState.Added)
                {
                    entityBase.CreatedAt = DateTime.Now;
                    entityBase.CreatedBy = "UTENTE";
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
