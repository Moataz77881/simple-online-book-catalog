﻿using simple_online_book_catalog.models.DTOModel.AuthorDTOs;

namespace simple_online_book_catalog.Services.IServices
{
    public interface IAuthorService
    {
        public Task<List<getAuthorDTO>> getAllAuthorService();
        public Task<getAuthorDTO> createAuthor(createAuthorDTO authorDTO);
        public Task<getAuthorDTO?> updateAuthor(Guid id, createAuthorDTO createAuthorDTO);
        public Task<getAuthorDTO> removeAuthor(Guid id);
    }
}
