using System;
using System.Collections.Generic;
using System.Text;

namespace Stands.Utility
{
    class CardCount
    {
		public static int Amount(Player player, string cardName)
		{
			List<CardInfo> currentCards = player.data.currentCards;
			int num = 0;
			for (int i = currentCards.Count - 1; i >= 0; i--)
			{
				bool flag = currentCards[i].cardName == cardName;
				if (flag)
				{
					num++;
				}
			}
			return num;
		}
	}
}
