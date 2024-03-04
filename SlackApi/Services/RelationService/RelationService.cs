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

        public async Task<IQueryable<Relation>> GetAllRelations()
        {
            return _unitOfWork1.RelationRepository.GetAll();
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

        public async Task<Relation> UpdateRelationById(long id, string type)
        {
            Relation r = (await _unitOfWork1.RelationRepository.Find(a => a.RelationId == id)).FirstOrDefault() ?? throw new NullReferenceException();

            r.RelationType = type;

                 await   _unitOfWork1.RelationRepository.Update(r);
            return r;



        }

        public async Task<Relation> GetRelationByBothId(long userID1, long userID2)
        {

           var r = await  _unitOfWork1.RelationRepository.Find(A => (A.UserId1 == userID1 && A.UserId2 == userID2) || (A.UserId2 == userID1 && A.UserId1 == userID2));
            Relation data = r.FirstOrDefault();

            return data;

        }
    }
}
