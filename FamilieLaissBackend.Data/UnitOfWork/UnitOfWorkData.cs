using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Data.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.UnitOfWork
{
    public class UnitOfWorkData: iUnitOfWorkData
    {
        #region Private Members
        private EFContextProvider<FamilieLaissEntities> contextProvider;
        #endregion

        #region C'tor
        /// <summary>
        /// ctor
        /// </summary>
        public UnitOfWorkData(iBreezeValidator breezevalidator)
        {
            //Erstellen des Context-Providers für Breeze
            contextProvider = new EFContextProvider<FamilieLaissEntities>();
            contextProvider.BeforeSaveEntitiesDelegate = breezevalidator.BeforeSaveEntities;
            contextProvider.BeforeSaveEntityDelegate = breezevalidator.BeforeSaveEntity;
            contextProvider.AfterSaveEntitiesDelegate = breezevalidator.AfterSaveEntities;
          
            //Erstellen der Repositories
            MediaGroupRepository = new RepositoryData<MediaGroup>(contextProvider.Context);
            FacetGroupRepository = new RepositoryData<FacetGroup>(contextProvider.Context);
            FacetValueRepository = new RepositoryData<FacetValue>(contextProvider.Context);
            MediaItemRepository = new RepositoryData<MediaItem>(contextProvider.Context);
            MediaItemFacetRepository = new RepositoryData<MediaItemFacet>(contextProvider.Context);
            UploadPictureItemRepository = new RepositoryData<UploadPictureItem>(contextProvider.Context);
            UploadPictureImagePropertyRepository = new RepositoryData<UploadPictureImageProperty>(contextProvider.Context);
            UploadVideoItemRepository = new RepositoryData<UploadVideoItem>(contextProvider.Context);
            //VideoConvertStatusRepository = new Repository<Video_Convert_Status>(contextProvider.Context);
            //MessageRepository = new Repository<Message>(contextProvider.Context);
        }
        #endregion

        #region Public Properties for Repositories
        public iRepositoryData<MediaGroup> MediaGroupRepository { get; private set; }
        public iRepositoryData<FacetGroup> FacetGroupRepository { get; private set; }
        public iRepositoryData<FacetValue> FacetValueRepository { get; private set; }
        public iRepositoryData<MediaItem> MediaItemRepository { get; private set; }
        public iRepositoryData<MediaItemFacet> MediaItemFacetRepository { get; private set; }
        public iRepositoryData<UploadPictureItem> UploadPictureItemRepository { get; private set; }
        public iRepositoryData<UploadPictureImageProperty> UploadPictureImagePropertyRepository { get; private set; }
        public iRepositoryData<UploadVideoItem> UploadVideoItemRepository { get; private set; }
        //public iRepositoryData<Video_Convert_Status> VideoConvertStatusRepository { get; private set; }
        //public iRepositoryData<Message> MessageRepository { get; private set; }
        #endregion

        #region Breeze Methoden
        /// <summary>
        /// Get breeze Metadata
        /// </summary>
        /// <returns>String containing Breeze metadata</returns>
        public string Metadata()
        {
            return contextProvider.Metadata();
        }

        /// <summary>
        /// Save a changeset using Breeze
        /// </summary>
        /// <param name="changeSet"></param>
        /// <returns></returns>
        public SaveResult Commit(JObject changeSet)
        {
            return contextProvider.SaveChanges(changeSet);
        }
        #endregion
    }
}
