using FamilieLaissAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAPI.Data.Context
{
    public class FamilieLaissDBContext: DbContext
    {
        //Benutze den Connection-String mit dem Namen FamilieLaissDBContext
        public FamilieLaissDBContext() : base("FamilieLaissDBContext")
        {
        }

        //Deklarieren der Entity-Sets
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<FacetGroup> FacetGroups { get; set; }
        public DbSet<FacetValue> FacetValues { get; set; }
        public DbSet<MediaGroup> MediaGroups { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<UploadPictureItem> UploadPictureItems { get; set; }
        public DbSet<UploadVideoItem> UploadVideoItems { get; set; }
        public DbSet<UploadPictureItemExif> UploadPictureItemExifs { get; set; }
        public DbSet<UploadPictureItemProperty> UploadPictureItemProperty { get; set; }
        public DbSet<MediaItemFacet> MediaItemFacets { get; set; }

        //Über die Fluent-API die Erstellung der Datenbank steuern. Der Rest wird über die
        //Data-Annotations in den jeweiligen Models geregelt (Key, Maxlength, Require, etc.)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Pluralize für Tabellennamen deaktivieren
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Festlgen das alle Entity-Sets mit Stored Proceduew-Mapping arbeiten sollen
            modelBuilder.Entity<Blog>().MapToStoredProcedures();
            modelBuilder.Entity<FacetGroup>().MapToStoredProcedures();
            modelBuilder.Entity<FacetValue>().MapToStoredProcedures();
            modelBuilder.Entity<MediaGroup>().MapToStoredProcedures();
            modelBuilder.Entity<MediaItem>().MapToStoredProcedures();
            modelBuilder.Entity<UploadPictureItem>().MapToStoredProcedures();
            modelBuilder.Entity<UploadVideoItem>().MapToStoredProcedures();
            modelBuilder.Entity<UploadPictureItemExif>().MapToStoredProcedures();
            modelBuilder.Entity<UploadPictureItemProperty>().MapToStoredProcedures();
            modelBuilder.Entity<MediaItemFacet>().MapToStoredProcedures();

            //Festlegen des Foreign-Keys für die Facet-Value / Facet-Group Beziehung
            modelBuilder.Entity<FacetGroup>()
                .HasMany<FacetValue>(x => x.FacetValues)
                .WithRequired(s => s.FacetGroup)
                .WillCascadeOnDelete();

            //Festlegen des Foreign-Keys für die Media-Item / Media-Group Beziehung
            modelBuilder.Entity<MediaGroup>()
                .HasMany<MediaItem>(x => x.MediaItems)
                .WithRequired(s => s.MediaGroup)
                .WillCascadeOnDelete();

            //Festlegen des Foreign-Keys für die Upload-Picture-Item / Media-Item Beziehung
            modelBuilder.Entity<UploadPictureItem>()
                .HasMany<MediaItem>(x => x.MediaItems)
                .WithOptional(s => s.UploadPictureItem);

            //Festlegen des Foreign-Keys für die Upload-Video-Item / Media-Item Beziehung
            modelBuilder.Entity<UploadVideoItem>()
                .HasMany<MediaItem>(x => x.MediaItems)
                .WithOptional(s => s.UploadVideoItem);

            //Festlegen des Foreign-Keys für die Media-Item-Facet / Media-Item Beziehung
            modelBuilder.Entity<MediaItem>()
                .HasMany<MediaItemFacet>(x => x.MediaItemFacets)
                .WithRequired(s => s.MediaItem)
                .WillCascadeOnDelete();

            //Festlegen des Foreign-Keys für die Media-Item-Facet / Facet-Value Beziehung
            modelBuilder.Entity<FacetValue>()
                .HasMany<MediaItemFacet>(x => x.MediaItemFacets)
                .WithRequired(s => s.FacetValue)
                .WillCascadeOnDelete();

            //Festlegen des Foreign-Keys für die Upload-Picture-Exif / Upload-Picture Beziehung
            modelBuilder.Entity<UploadPictureItemExif>()
                .HasRequired<UploadPictureItem>(x => x.UploadPictureItem)
                .WithOptional(s => s.UploadPictureItemExif);

            //Festlegen des Foreign-Keys für die Upload-Picture-Exif / Upload-Picture Beziehung
            modelBuilder.Entity<UploadPictureItemProperty>()
                .HasRequired<UploadPictureItem>(x => x.UploadPictureItem)
                .WithOptional(s => s.UploadPictureItemProperty);
        }
    }
}
