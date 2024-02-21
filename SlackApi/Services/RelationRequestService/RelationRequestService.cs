using AutoMapper;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;

namespace SlackApi.Services.RelationRequestService
{
    public class RelationRequestService : IRelationRequestService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RelationRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            //_relationRequestRepository = relationRequestRepository;
            _mapper = mapper;
        }

        public async Task<RelationRequest> CreateRelationRequest(RelationRequestDto requestDto)
        {
             var userQuery = await  _unitOfWork.UserRepository.Find(a=>a.UserId== requestDto.UserId);
            var requestorQury = await _unitOfWork.UserRepository.Find(a => a.UserId == requestDto.RequestorId);

            var user = userQuery.FirstOrDefault();
            var requestor = requestorQury.FirstOrDefault();


            RelationRequest relationRequest = _mapper.Map<RelationRequest>(requestDto);
            relationRequest.User = user;
            relationRequest.Requestor = requestor;

            var data = await _unitOfWork.RelationRequestRepository.Insert(relationRequest);

            return data;


        }

        public async Task<bool> DeleteRelationRequest(long id)
        {
            return await _unitOfWork.RelationRequestRepository.Delete(id);
        }

        public async Task<IEnumerable<RelationRequest>> GetAllRelationRequestsByUserId(long id)
        {
            return await _unitOfWork.RelationRequestRepository.Find(a=>a.UserId ==id);
        }

       public async Task<IEnumerable<RelationRequest>> GetAllRelationRequestsByRequestorId(long id)
        {
            return await _unitOfWork.RelationRequestRepository.Find(a => a.Requestor.UserId == id);
        }

        public async Task<RelationRequest> GetRelationRequestById(long id)
        {
           var data =await _unitOfWork.RelationRequestRepository.Find(r => r.Id == id);

            return data.FirstOrDefault();
        }

        public async Task<bool> UpdateRelationRequest(UpdateRelationRequestDto requestDto)
        {
            var existingRelationRequestQuery = await _unitOfWork.RelationRequestRepository.Find(r => r.Id == requestDto.Id);
            var existingRelationRequest = existingRelationRequestQuery.FirstOrDefault();
            if (existingRelationRequest == null)
            {
               
                // Handle relation request not found scenario
                throw new Exception("Relation request not found.");
            }

           existingRelationRequest.RelationShip = requestDto.RelationShip;
            await _unitOfWork.RelationRequestRepository.Update(existingRelationRequest);
            return true;
        }
    }
}
