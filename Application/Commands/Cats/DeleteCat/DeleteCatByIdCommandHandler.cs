﻿using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.DeleteDog
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly MockDatabase _mockDatabase;
        public DeleteCatByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat catToDelete = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.DeletedCatId)!;

            if (catToDelete != null)
            {
                _mockDatabase.Cats.Remove(catToDelete);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}