using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Validator
{
    public class BreezeValidator : iBreezeValidator
    {
        #region Private Members
        private FamilieLaissEntities _DatabaseContext;
        #endregion

        #region C'tor
        public BreezeValidator()
        {
            _DatabaseContext = new FamilieLaissEntities();
        }
        #endregion

        #region Breeze Methods for Validation
        public bool BeforeSaveEntity(EntityInfo entityInfo)
        {
            return true;
        }

        public Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap)
        {
            //Deklrationen
            List<EntityInfo> facetGroupInfos;
            List<EntityInfo> facetValueInfos;
            List<EntityInfo> mediaGroupInfos;
            EFEntityInfo facetGroupConverted;
            EFEntityInfo facetValueConverted;
            EFEntityInfo mediaGroupConverted;
            //FacetGroup entityFacetGroup;
            //FacetValue entityFacetValue;
            MediaGroup entityMediaGroup;
            List<EFEntityError> errors = new List<EFEntityError>();

            //Überprüfen der Facet-Groups
            //if (saveMap.TryGetValue(typeof(FacetGroup), out facetGroupInfos))
            //{
            //    foreach (var facetGroup in facetGroupInfos)
            //    {
            //        facetGroupConverted = (EFEntityInfo)facetGroup;
            //        if (facetGroupConverted.EntityState == EntityState.Added || facetGroupConverted.EntityState == EntityState.Modified)
            //        {
            //            //Ermitteln der Entity
            //            entityFacetGroup = (FacetGroup)facetGroupConverted.Entity;

            //            //Überprüfen ob der deutsche Name schon existiert
            //            int Count = 0;
            //            if (facetGroupConverted.EntityState == EntityState.Added)
            //            {
            //                if (entityFacetGroup.Typ == 1 || entityFacetGroup.Typ == 2)
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.NameGerman == entityFacetGroup.NameGerman && x.Typ == entityFacetGroup.Typ);
            //                }
            //                else
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.NameGerman == entityFacetGroup.NameGerman && (x.Typ == 0 || x.Typ == entityFacetGroup.Typ));
            //                }
            //            }
            //            else
            //            {
            //                if (entityFacetGroup.Typ == 1 || entityFacetGroup.Typ == 2)
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.ID != entityFacetGroup.ID && x.NameGerman == entityFacetGroup.NameGerman && x.Typ == entityFacetGroup.Typ);
            //                }
            //                else
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.ID != entityFacetGroup.ID && x.NameGerman == entityFacetGroup.NameGerman && (x.Typ == 0 || x.Typ == entityFacetGroup.Typ));
            //                }
            //            }
            //            if (Count > 0) errors.Add(new EFEntityError(facetGroup, "DuplicateGermanName", Validation_Resources.Facet_Group_Duplicated_Value_German, "Name_German"));

            //            //Überprüfen bo der englische Name schon existiert
            //            Count = 0;
            //            if (facetGroupConverted.EntityState == EntityState.Added)
            //            {
            //                if (entityFacetGroup.Typ == 1 || entityFacetGroup.Typ == 2)
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.NameEnglish == entityFacetGroup.NameEnglish && x.Typ == entityFacetGroup.Typ);
            //                }
            //                else
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.NameEnglish == entityFacetGroup.NameEnglish && (x.Typ == 0 || x.Typ == entityFacetGroup.Typ));
            //                }
            //            }
            //            else
            //            {
            //                if (entityFacetGroup.Typ == 1 || entityFacetGroup.Typ == 2)
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.ID != entityFacetGroup.ID && x.NameEnglish == entityFacetGroup.NameEnglish && x.Typ == entityFacetGroup.Typ);
            //                }
            //                else
            //                {
            //                    Count = _DatabaseContext.FacetGroups.Count(x => x.ID != entityFacetGroup.ID && x.NameEnglish == entityFacetGroup.NameEnglish && (x.Typ == 0 || x.Typ == entityFacetGroup.Typ));
            //                }
            //            }
            //            if (Count > 0) errors.Add(new EFEntityError(facetGroup, "DuplicateEnglishName", Validation_Resources.Facet_Group_Duplicated_Value_English, "Name_English"));

            //            //Setzen von Standardwerten für die Facet-Groups
            //            if (facetGroupConverted.EntityState == EntityState.Added)
            //            {
            //                entityFacetGroup.FacetValueType = 2;
            //                entityFacetGroup.NotDeleteable = false;
            //            }
            //        }
            //    }
            //}

            //Überprüfen der Facet-Values
            //if (saveMap.TryGetValue(typeof(FacetValue), out facetValueInfos))
            //{
            //    foreach (var facetValue in facetValueInfos)
            //    {
            //        facetValueConverted = (EFEntityInfo)facetValue;
            //        if (facetValueConverted.EntityState == EntityState.Added || facetValueConverted.EntityState == EntityState.Modified)
            //        {
            //            //Ermitteln der Entity
            //            entityFacetValue = (FacetValue)facetValueConverted.Entity;

            //            //Überprüfen ob der deutsche Name schon existiert
            //            int Count = 0;
            //            if (facetValueConverted.EntityState == EntityState.Added)
            //            {
            //                Count = _DatabaseContext.FacetValues.Count(
            //                    x => x.NameGerman == entityFacetValue.NameGerman &&
            //                    x.IDGroup == entityFacetValue.IDGroup);
            //            }
            //            else
            //            {
            //                Count = _DatabaseContext.FacetValues.Count(
            //                    x => x.ID != entityFacetValue.ID &&
            //                    x.NameGerman == entityFacetValue.NameGerman &&
            //                    x.IDGroup == entityFacetValue.IDGroup);
            //            }
            //            if (Count > 0) errors.Add(new EFEntityError(facetValue, "DuplicateGermanName", Validation_Resources.Facet_Value_Duplicated_Value_German, "Name_German"));

            //            //Überprüfen ob der englische Name schon existiert
            //            Count = 0;
            //            if (facetValueConverted.EntityState == EntityState.Added)
            //            {
            //                Count = _DatabaseContext.FacetValues.Count(
            //                    x => x.NameEnglish == entityFacetValue.NameEnglish &&
            //                    x.IDGroup == entityFacetValue.IDGroup);
            //            }
            //            else
            //            {
            //                Count = _DatabaseContext.FacetValues.Count(
            //                    x => x.ID != entityFacetValue.ID &&
            //                    x.NameEnglish == entityFacetValue.NameEnglish &&
            //                    x.IDGroup == entityFacetValue.IDGroup);
            //            }
            //            if (Count > 0) errors.Add(new EFEntityError(facetValue, "DuplicateEnglishName", Validation_Resources.Facet_Value_Duplicated_Value_English, "Name_English"));
            //        }
            //    }
            //}

            //Überprüfen der Media-Groups
            //if (saveMap.TryGetValue(typeof(MediaGroup), out mediaGroupInfos))
            //{
            //    foreach (var mediaGroup in mediaGroupInfos)
            //    {
            //        mediaGroupConverted = (EFEntityInfo)mediaGroup;
            //        if (mediaGroupConverted.EntityState == EntityState.Added || mediaGroupConverted.EntityState == EntityState.Modified)
            //        {
            //            //Ermitteln der Entity
            //            entityMediaGroup = (MediaGroup)mediaGroupConverted.Entity;

            //            //Überprüfen ob der Name schon existiert
            //            int Count = 0;
            //            if (mediaGroupConverted.EntityState == EntityState.Added)
            //            {
            //                Count = _DatabaseContext.MediaGroups.Count(
            //                    x => x.Name == entityMediaGroup.Name &&
            //                    x.Typ == entityMediaGroup.Typ);
            //            }
            //            else
            //            {
            //                Count = _DatabaseContext.MediaGroups.Count(
            //                    x => x.ID != entityMediaGroup.ID &&
            //                    x.Name == entityMediaGroup.Name &&
            //                    x.Typ == entityMediaGroup.Typ);
            //            }
            //            if (Count > 0) errors.Add(new EFEntityError(mediaGroup, "DuplicateName", Validation_Resources.Media_Group_Duplicated_Value_Name, "Name"));

            //            //Setzen der Standardwerte für die Media-Group
            //            if (mediaGroupConverted.EntityState == EntityState.Added)
            //            {
            //                entityMediaGroup.CreateDate = DateTime.Now;
            //            }
            //        }
            //    }
            //}

            //Wenn Validierungsfehler existieren, dann wird die entsprechende Exception geworfen
            if (errors.Count > 0)
            {
                throw new EntityErrorsException(errors);
            }

            //Funktionsergebnis
            return saveMap;
        }
        #endregion
    }
}
