﻿namespace Application.Dtos
{
    public class CatDto
    {
        public string Name { get; set; } = string.Empty;
        public bool LikesToPlay { get; set; }
        public string CatBreed { get; set; } = string.Empty;
        public int CatWeight { get; set; }
    }
}
