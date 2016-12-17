using UnityEngine;
using System.Collections;

public static class FisherYaterShuffler {
		
		public static void DoShuffle(this object[] Array)
		{
		
			for ( int i = Array.Length - 1; i > 0; --i )
			{
				int j = Random.Range (0,Array.Length);
				object temp = Array[i];
				Array [i] = Array[j];
				Array [j] = temp;
			}
		}
	
		static void Main (string[] args) {
		
			string[] letters = {"a", "b", "c", "d", "e"};
			letters.DoShuffle();

			foreach (string letter in letters)
			
				Debug.Log (letter + " ");
			
				



		}
	}
