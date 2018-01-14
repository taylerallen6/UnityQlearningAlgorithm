using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QLearner
{
	public class QLearnerScript
	{
		private List<float[]> QtStates;
		private List<float[]> QtActions;

		private int possibleActions;

		private float[] initialState;
		private int initialActionIndex;
		private float[] outcomeState;
		private float outcomeActionValue;

		private float lr;
		private float y;

		private float SimInterval;

		private bool firstIteration;
		System.Random random = new System.Random();

		public QLearnerScript(int possActs)
		{
			QtStates = new List<float[]>();
			QtActions = new List<float[]>();
			possibleActions = possActs;

			lr = .8f;
			y = .95f;

			SimInterval = 1f;

			firstIteration = true;
		}

		



		public int getQtableCount()
		{
			return QtStates.Count;
		}

		public int main(float[] curState, float reward)
		{
			step2(curState, reward);


			initialState = new float[2] {(float)Math.Round((double)curState[0] / SimInterval) * SimInterval , (float)Math.Round((double)curState[1] / SimInterval) * SimInterval};
			// initialState = curState;

			firstIteration = false;

			int actionIndex = random.Next(0, possibleActions);

			bool exists = false;
			if(QtStates.Count > 0)
			{
				// foreach(float[][] state in Qtable)
				for(int i = 0; i < QtStates.Count; i++)
				{
					float[] state = QtStates.ElementAt(i);
					float[] actions = QtActions.ElementAt(i);

					if(state[0] == initialState[0] && state[1] == initialState[1])
					{
						exists = true;
						initialActionIndex = Array.IndexOf(actions, MaxFloat(actions));
						Debug.Log("       picked action value:          " + actions[initialActionIndex]);

						return initialActionIndex;
					}
				}
			}

			if(!exists)
			{
				float[] actionVals = new float[possibleActions];
				for (int i = 0; i < possibleActions; i++)
				{
					actionVals[i] = 0f;
				}
				QtStates.Add(initialState);
				QtActions.Add(actionVals);
			}

			initialActionIndex = actionIndex;
			return initialActionIndex;
		}

		public void step2(float[] outcmState, float reward)
		{
			if(!firstIteration)
			{
				outcomeState = new float[2] {(float)Math.Round((double)outcmState[0] / SimInterval) * SimInterval , (float)Math.Round((double)outcmState[1] / SimInterval) * SimInterval};
				// outcomeState = outcmState;

				bool exists = false;
				for(int i = 0; i < QtStates.Count; i++)
				{
					float[] state = QtStates.ElementAt(i);
					float[] actions = QtActions.ElementAt(i);

					if(state[0] == outcomeState[0] && state[1] == outcomeState[1])
					{
						exists = true;
						outcomeActionValue = MaxFloat(actions);
					}
				}

				for(int i = 0; i < QtStates.Count; i++)
				{
					float[] state = QtStates.ElementAt(i);
					float[] actions = QtActions.ElementAt(i);

					if(state[0] == initialState[0] && state[1] == initialState[1])
					{
						
						if(exists)
						{
							actions[initialActionIndex] = (actions[initialActionIndex] + lr * (reward + y * outcomeActionValue - actions[initialActionIndex]));
						}

						if(!exists)
						{
							actions[initialActionIndex] = (actions[initialActionIndex] + lr * (reward + y * 0f - actions[initialActionIndex]));
						}
					}
				}
			}
		}

		private float MaxFloat(float[] numbers)
        {
            float m = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
                if (m < numbers[i])
                {
                    m = numbers[i];
                }

            return m;
        }
	}
}