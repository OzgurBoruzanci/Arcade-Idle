using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NEW SalesDeskUpdateSC", menuName = "ScriptableObjects/SalesDeskUpdateScriptableObject")]
public class SalesDeskUpdateScriptableObject : ScriptableObject
{
    public enum Levels { one,two,three};
    public Levels level;
    public float waitingseconds;
    public int limitToBeProduced;
    public int productSalesFee;


    public void LevelSelection()
    {
        switch (level)
        {
            case Levels.one:
                waitingseconds = 10;
                limitToBeProduced = 5;
                productSalesFee = 4;
                break;
            case Levels.two:
                waitingseconds = 8;
                limitToBeProduced = 6;
                productSalesFee = 2;
                break;
            case Levels.three:
                waitingseconds = 5;
                limitToBeProduced = 7;
                productSalesFee = 3;
                break;
        }
    }
}
