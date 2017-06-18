using FamilieLaissBackend.Data.Model;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Interface
{
    public interface iUnitOfWorkData
    {
        //Das Repository für die Alben
        iRepositoryData<MediaGroup> MediaGroupRepository { get; }
        iRepositoryData<FacetGroup> FacetGroupRepository { get; }
        iRepositoryData<FacetValue> FacetValueRepository { get; }
        iRepositoryData<MediaItem> MediaItemRepository { get; }
        iRepositoryData<MediaItemFacet> MediaItemFacetRepository { get; }
        iRepositoryData<UploadPictureItem> UploadPictureItemRepository { get; }
        iRepositoryData<UploadPictureImageProperty> UploadPictureImagePropertyRepository { get; }
        iRepositoryData<UploadVideoItem> UploadVideoItemRepository { get; }
        //iDataRepository<Video_Convert_Status> VideoConvertStatusRepository { get; }
        //iDataRepository<Message> MessageRepository { get; }

        //Die Änderungen in der Datenbank speichern
        Task<int> SaveChanges();
    }
}
