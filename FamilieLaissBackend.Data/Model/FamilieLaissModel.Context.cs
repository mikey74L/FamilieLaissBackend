﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FamilieLaissBackend.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FamilieLaissEntities : DbContext
    {
        public FamilieLaissEntities()
            : base("name=FamilieLaissEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MediaGroup> MediaGroups { get; set; }
        public virtual DbSet<FacetGroup> FacetGroups { get; set; }
        public virtual DbSet<FacetValue> FacetValues { get; set; }
        public virtual DbSet<MediaItem> MediaItems { get; set; }
        public virtual DbSet<MediaItemFacet> MediaItemFacets { get; set; }
        public virtual DbSet<UploadPictureImageProperty> UploadPictureImageProperties { get; set; }
        public virtual DbSet<UploadPictureItem> UploadPictureItems { get; set; }
        public virtual DbSet<UploadVideoItem> UploadVideoItems { get; set; }
    
        public virtual int sp_Media_Group_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Media_Group_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Media_Group_Insert_Result> sp_Media_Group_Insert(Nullable<byte> p_Typ, string p_Name_German, string p_Name_English, string p_Description_German, string p_Description_English)
        {
            var p_TypParameter = p_Typ.HasValue ?
                new ObjectParameter("p_Typ", p_Typ) :
                new ObjectParameter("p_Typ", typeof(byte));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            var p_Description_GermanParameter = p_Description_German != null ?
                new ObjectParameter("p_Description_German", p_Description_German) :
                new ObjectParameter("p_Description_German", typeof(string));
    
            var p_Description_EnglishParameter = p_Description_English != null ?
                new ObjectParameter("p_Description_English", p_Description_English) :
                new ObjectParameter("p_Description_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Media_Group_Insert_Result>("sp_Media_Group_Insert", p_TypParameter, p_Name_GermanParameter, p_Name_EnglishParameter, p_Description_GermanParameter, p_Description_EnglishParameter);
        }
    
        public virtual int sp_Media_Group_Update(Nullable<long> p_ID, string p_Name_German, string p_Name_English, string p_Description_German, string p_Description_English)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            var p_Description_GermanParameter = p_Description_German != null ?
                new ObjectParameter("p_Description_German", p_Description_German) :
                new ObjectParameter("p_Description_German", typeof(string));
    
            var p_Description_EnglishParameter = p_Description_English != null ?
                new ObjectParameter("p_Description_English", p_Description_English) :
                new ObjectParameter("p_Description_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Media_Group_Update", p_IDParameter, p_Name_GermanParameter, p_Name_EnglishParameter, p_Description_GermanParameter, p_Description_EnglishParameter);
        }
    
        public virtual int sp_Facet_Group_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Facet_Group_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Facet_Group_Insert_Result> sp_Facet_Group_Insert(Nullable<byte> p_Type, string p_Name_German, string p_Name_English)
        {
            var p_TypeParameter = p_Type.HasValue ?
                new ObjectParameter("p_Type", p_Type) :
                new ObjectParameter("p_Type", typeof(byte));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Facet_Group_Insert_Result>("sp_Facet_Group_Insert", p_TypeParameter, p_Name_GermanParameter, p_Name_EnglishParameter);
        }
    
        public virtual int sp_Facet_Group_Update(Nullable<long> p_ID, string p_Name_German, string p_Name_English)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Facet_Group_Update", p_IDParameter, p_Name_GermanParameter, p_Name_EnglishParameter);
        }
    
        public virtual int sp_Facet_Value_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Facet_Value_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Facet_Value_Insert_Result> sp_Facet_Value_Insert(Nullable<long> p_ID_Group, string p_Name_German, string p_Name_English)
        {
            var p_ID_GroupParameter = p_ID_Group.HasValue ?
                new ObjectParameter("p_ID_Group", p_ID_Group) :
                new ObjectParameter("p_ID_Group", typeof(long));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Facet_Value_Insert_Result>("sp_Facet_Value_Insert", p_ID_GroupParameter, p_Name_GermanParameter, p_Name_EnglishParameter);
        }
    
        public virtual int sp_Facet_Value_Update(Nullable<long> p_ID, string p_Name_German, string p_Name_English)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Facet_Value_Update", p_IDParameter, p_Name_GermanParameter, p_Name_EnglishParameter);
        }
    
        public virtual int sp_Media_Item_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Media_Item_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Media_Item_Insert_Result> sp_Media_Item_Insert(Nullable<long> p_ID_Group, Nullable<int> p_Typ, string p_Name_German, string p_Name_English, string p_Description_German, string p_Description_English, Nullable<bool> p_Only_Family, Nullable<long> p_Upload_Picture, Nullable<long> p_Upload_Video)
        {
            var p_ID_GroupParameter = p_ID_Group.HasValue ?
                new ObjectParameter("p_ID_Group", p_ID_Group) :
                new ObjectParameter("p_ID_Group", typeof(long));
    
            var p_TypParameter = p_Typ.HasValue ?
                new ObjectParameter("p_Typ", p_Typ) :
                new ObjectParameter("p_Typ", typeof(int));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            var p_Description_GermanParameter = p_Description_German != null ?
                new ObjectParameter("p_Description_German", p_Description_German) :
                new ObjectParameter("p_Description_German", typeof(string));
    
            var p_Description_EnglishParameter = p_Description_English != null ?
                new ObjectParameter("p_Description_English", p_Description_English) :
                new ObjectParameter("p_Description_English", typeof(string));
    
            var p_Only_FamilyParameter = p_Only_Family.HasValue ?
                new ObjectParameter("p_Only_Family", p_Only_Family) :
                new ObjectParameter("p_Only_Family", typeof(bool));
    
            var p_Upload_PictureParameter = p_Upload_Picture.HasValue ?
                new ObjectParameter("p_Upload_Picture", p_Upload_Picture) :
                new ObjectParameter("p_Upload_Picture", typeof(long));
    
            var p_Upload_VideoParameter = p_Upload_Video.HasValue ?
                new ObjectParameter("p_Upload_Video", p_Upload_Video) :
                new ObjectParameter("p_Upload_Video", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Media_Item_Insert_Result>("sp_Media_Item_Insert", p_ID_GroupParameter, p_TypParameter, p_Name_GermanParameter, p_Name_EnglishParameter, p_Description_GermanParameter, p_Description_EnglishParameter, p_Only_FamilyParameter, p_Upload_PictureParameter, p_Upload_VideoParameter);
        }
    
        public virtual int sp_Media_Item_Update(Nullable<long> p_ID, string p_Name_German, string p_Name_English, string p_Description_German, string p_Description_English, Nullable<bool> p_Only_Family)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_Name_GermanParameter = p_Name_German != null ?
                new ObjectParameter("p_Name_German", p_Name_German) :
                new ObjectParameter("p_Name_German", typeof(string));
    
            var p_Name_EnglishParameter = p_Name_English != null ?
                new ObjectParameter("p_Name_English", p_Name_English) :
                new ObjectParameter("p_Name_English", typeof(string));
    
            var p_Description_GermanParameter = p_Description_German != null ?
                new ObjectParameter("p_Description_German", p_Description_German) :
                new ObjectParameter("p_Description_German", typeof(string));
    
            var p_Description_EnglishParameter = p_Description_English != null ?
                new ObjectParameter("p_Description_English", p_Description_English) :
                new ObjectParameter("p_Description_English", typeof(string));
    
            var p_Only_FamilyParameter = p_Only_Family.HasValue ?
                new ObjectParameter("p_Only_Family", p_Only_Family) :
                new ObjectParameter("p_Only_Family", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Media_Item_Update", p_IDParameter, p_Name_GermanParameter, p_Name_EnglishParameter, p_Description_GermanParameter, p_Description_EnglishParameter, p_Only_FamilyParameter);
        }
    
        public virtual int sp_MediaFacet_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_MediaFacet_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<Nullable<long>> sp_MediaFacet_Insert(Nullable<long> p_Media_ID, Nullable<long> p_FacetValue)
        {
            var p_Media_IDParameter = p_Media_ID.HasValue ?
                new ObjectParameter("p_Media_ID", p_Media_ID) :
                new ObjectParameter("p_Media_ID", typeof(long));
    
            var p_FacetValueParameter = p_FacetValue.HasValue ?
                new ObjectParameter("p_FacetValue", p_FacetValue) :
                new ObjectParameter("p_FacetValue", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("sp_MediaFacet_Insert", p_Media_IDParameter, p_FacetValueParameter);
        }
    
        public virtual int sp_MediaFacet_Update(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_MediaFacet_Update", p_IDParameter);
        }
    
        public virtual int sp_Upload_Picture_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Upload_Picture_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Upload_Picture_Insert_Result> sp_Upload_Picture_Insert(string p_Name, Nullable<int> p_Height, Nullable<int> p_Width)
        {
            var p_NameParameter = p_Name != null ?
                new ObjectParameter("p_Name", p_Name) :
                new ObjectParameter("p_Name", typeof(string));
    
            var p_HeightParameter = p_Height.HasValue ?
                new ObjectParameter("p_Height", p_Height) :
                new ObjectParameter("p_Height", typeof(int));
    
            var p_WidthParameter = p_Width.HasValue ?
                new ObjectParameter("p_Width", p_Width) :
                new ObjectParameter("p_Width", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Upload_Picture_Insert_Result>("sp_Upload_Picture_Insert", p_NameParameter, p_HeightParameter, p_WidthParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_Upload_Picture_Update(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Upload_Picture_Update", p_IDParameter);
        }
    
        public virtual int sp_Upload_Video_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Upload_Video_Delete", p_IDParameter);
        }
    
        public virtual ObjectResult<sp_Upload_Video_Insert_Result> sp_Upload_Video_Insert(string p_Name)
        {
            var p_NameParameter = p_Name != null ?
                new ObjectParameter("p_Name", p_Name) :
                new ObjectParameter("p_Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Upload_Video_Insert_Result>("sp_Upload_Video_Insert", p_NameParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_Upload_Video_Update(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Upload_Video_Update", p_IDParameter);
        }
    
        public virtual int sp_UploadPictureImageProperty_Delete(Nullable<long> p_ID)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UploadPictureImageProperty_Delete", p_IDParameter);
        }
    
        public virtual int sp_UploadPictureImageProperty_Insert(Nullable<long> p_ID, Nullable<int> p_Rotation)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_RotationParameter = p_Rotation.HasValue ?
                new ObjectParameter("p_Rotation", p_Rotation) :
                new ObjectParameter("p_Rotation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UploadPictureImageProperty_Insert", p_IDParameter, p_RotationParameter);
        }
    
        public virtual int sp_UploadPictureImageProperty_Update(Nullable<long> p_ID, Nullable<int> p_Rotation)
        {
            var p_IDParameter = p_ID.HasValue ?
                new ObjectParameter("p_ID", p_ID) :
                new ObjectParameter("p_ID", typeof(long));
    
            var p_RotationParameter = p_Rotation.HasValue ?
                new ObjectParameter("p_Rotation", p_Rotation) :
                new ObjectParameter("p_Rotation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UploadPictureImageProperty_Update", p_IDParameter, p_RotationParameter);
        }
    }
}