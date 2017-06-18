using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Data.Repository;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.UnitOfWork
{
    public class UnitOfWorkData: iUnitOfWorkData
    {
        #region Private Members
        private FamilieLaissEntities _Context;
        #endregion

        #region C'tor
        /// <summary>
        /// ctor
        /// </summary>
        public UnitOfWorkData()
        {
            //Erstellen des DB-Context
            _Context = new FamilieLaissEntities();

            //Erstellen der Repositories
            MediaGroupRepository = new RepositoryData<MediaGroup>(_Context);
            FacetGroupRepository = new RepositoryData<FacetGroup>(_Context);
            FacetValueRepository = new RepositoryData<FacetValue>(_Context);
            MediaItemRepository = new RepositoryData<MediaItem>(_Context);
            MediaItemFacetRepository = new RepositoryData<MediaItemFacet>(_Context);
            UploadPictureItemRepository = new RepositoryData<UploadPictureItem>(_Context);
            UploadPictureImagePropertyRepository = new RepositoryData<UploadPictureImageProperty>(_Context);
            UploadVideoItemRepository = new RepositoryData<UploadVideoItem>(_Context);
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

        #region EF-Handler
        /// <summary>
        /// Save changes to database
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChanges()
        {
            return _Context.SaveChangesAsync();
        }
        #endregion
    }
}
