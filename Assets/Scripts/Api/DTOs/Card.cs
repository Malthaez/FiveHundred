﻿using MatchingGame.Api.DTOs.Enums;

namespace MatchingGame.Api.DTOs
{
    public class Card
    {
        public CardSuit? CardValue { get; set; }
        public string CardText { get; set; }
    }
}
