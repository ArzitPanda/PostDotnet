using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Exceptions;

namespace SlackApi.Services.RelationService
{
    public class RelationService:IRelationService
    {

        private readonly IUnitOfWork  _unitOfWork1;

        public RelationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public async Task<Relation> CreateRelation(RelationCreateDto relationDto)
        {
            // Map DTO to entity
         
            var Receiverdata = await _unitOfWork1.UserRepository.Find(a => a.UserId == relationDto.Receiver);
            var Senderdata = await _unitOfWork1.UserRepository.Find(a => a.UserId == relationDto.Sender);
            var  Receiever = Receiverdata.FirstOrDefault();
            var Sender = Senderdata.FirstOrDefault();

       
            if (Receiever == null || Sender ==null)
            {
                throw new  UserNotFoundException();
            }


            var relation = new Relation
            {
                UserId1 = relationDto.Receiver,
                UserId2 = relationDto.Sender,
                RelationType = relationDto.RelationType,
                User1=Receiever,
                User2=Sender
            };




            // Insert relation into repository
            return await _unitOfWork1.RelationRepository.Insert(relation);
        }

        public async Task<IEnumerable<Relation>> GetAllRelations()
        {
            return await _unitOfWork1.RelationRepository.GetAll();
        }

        public async Task<Relation> GetRelationById(long relationId)
        {
            return (await _unitOfWork1.RelationRepository.Find(r => r.RelationId == relationId)).FirstOrDefault();
        }

        public async Task<IEnumerable<Relation>> GetRelationsBySenderId(long senderId)
        {
            return await _unitOfWork1.RelationRepository.Find(r => r.UserId2 == senderId);
        }

        public async Task<IEnumerable<Relation>> GetRelationsByReceiverId(long receiverId)
        {
            return await _unitOfWork1.RelationRepository.Find(r => r.UserId1 == receiverId);
        }


    }
}
