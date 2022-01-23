using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using AutoMapper;

namespace Business.Services.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public SubscriptionDto CreateSubscription(SubscriptionDto subscription)
        {
            var entity = _mapper.Map<Subscription>(subscription);

            _subscriptionRepository.Create(entity);
            return _mapper.Map<SubscriptionDto>(entity);
        }

        public SubscriptionDto CreateOrUpdateSubscription(SubscriptionDto subscription)
        {
            var entity = _mapper.Map<Subscription>(subscription);

            _subscriptionRepository.CreateOrUpdate(entity);
            return _mapper.Map<SubscriptionDto>(entity);
        }

        public SubscriptionDto UpdateSubscription(SubscriptionDto newsubscription)
        {
            var entity = _mapper.Map<Subscription>(newsubscription);

            _subscriptionRepository.Update(entity);
            return _mapper.Map<SubscriptionDto>(entity);
        }

        public void DeleteSubscription(int index)
        {
            _subscriptionRepository.Delete(_subscriptionRepository.Query().Where(s => s.Id == index));
        }

        public IEnumerable<SubscriptionDto> FindByReaderId(int index)
        {
            return _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDto>>(_subscriptionRepository.Query().Where(s => s.Owner.Id == index));
        }

        public IEnumerable<SubscriptionDto> FindByTypeId(int index)
        {
            return _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDto>>(_subscriptionRepository.Query().Where(s => s.Type.Id == index));
        }

        public IEnumerable<SubscriptionDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDto>>(_subscriptionRepository.Query());
        }
    }
}
