using MatchingGame.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MatchingGame.Interfaces
{
    public interface IDealer
    {
        IEnumerator Deal(List<Dealable> dealables, Deck deck, int[] dealCount, Action<Dealable, Card> onSuccessfulDeal, Func<bool> continueDeal, Func<Dealable, bool> skipDealable);

        IEnumerator Deal(List<Dealable> dealables, Deck deck, int dealCount, Action<Dealable, Card> onSuccessfulDeal, Func<bool> continueDeal, Func<Dealable, bool> skipDealable);
    }
}
