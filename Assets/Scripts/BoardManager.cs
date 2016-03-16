using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.



public class BoardManager : MonoBehaviour
{
    public enum CompanyEnum
    {
        ALCOA,
        BANK_OF_NSW,
        AMPOL,
        BHP,
        WESTERN_MINING,
        COLES,
        CONSOLIDATED_PRESS,
        WOOLWORTHS,
        CORNER,
        START
    }
    public enum WorkEnum
    {
        DOCTOR,
        DEEP_SEA_DIVER,
        PROSPECTOR,
        POLICE_OFFICER
    }


    [Serializable]
    public class TradingSquare
    {
        public CompanyEnum Company;
        public int Steps;
        public int Dividend;
        public Color Colour;
        public int Rotation;
        public int Direction;
        public bool CanBuy;
        public bool MustSell;
        public bool Meeting;
        public float Fee;
        public Vector3 Centre;
        public float Width;
        public float Height;
        public float ViewingAngle;

        //public enum CompanyEnum
        //{
        //    ALCOA,
        //    BANK_OF_NSW,
        //    AMPOL,
        //    BHP,
        //    WESTERN_MINING,
        //    COLES,
        //    CONSOLIDATED_PRESS,
        //    WOOLWORTHS,
        //    CORNER,
        //    START
        //}

    }

    [Serializable]
    public class MeetingSquare
    {
        public CompanyEnum Company;
        public Vector3 Centre;
        public float Multiplier;
        public float Offset;


    }

    [Serializable]
    public class WorkSquare
    {
        public WorkEnum Job;
        public String Name;
        public int Salary;
        public Vector3 Centre;
        public List<int> WinRolls = new List<int>();


    }


    public int columns = 13;
    public int rows = 13;
    public GameObject Slider;

    public float YHeight = 2f;

    //public PlayerManager[] Players;

    //private Transform boardHolder;                                          //A variable to store a reference to the transform of our Board object.
    public List<TradingSquare> TradeSquares = new List<TradingSquare>();   //A list of possible locations to place players.
    public List<MeetingSquare> MeetingSquares = new List<MeetingSquare>(); //A list of possible locations to place players.
    public List<WorkSquare> WorkSquares = new List<WorkSquare>();          //A list of possible locations to place players.

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        TradeSquares.Clear();
        MeetingSquares.Clear();

        // Add the 48 trade squares
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CORNER, Rotation = 45, Direction = -1, Steps = 20, Dividend = 0, Fee = 0.1f, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 315f, Centre = new Vector3(-18, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WOOLWORTHS, Rotation = 90, Direction = -1, Steps = 5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 307.5f, Centre = new Vector3(-18, YHeight, -15) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BANK_OF_NSW, Rotation = 90, Direction = -1, Steps = -4, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 300f, Centre = new Vector3(-18, YHeight, -12) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.ALCOA, Rotation = 90, Direction = -1, Steps = 3, Dividend = 4, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 292.5f, Centre = new Vector3(-18, YHeight, -9) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BHP, Rotation = 90, Direction = 1, Steps = -2, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 285f, Centre = new Vector3(-18, YHeight, -6) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.COLES, Rotation = 90, Direction = 1, Steps = 1, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 277.5f, Centre = new Vector3(-18, YHeight, -3) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.START, Rotation = 90, Direction = 0, Steps = 0, Dividend = 0, Fee = 100, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 270f, Centre = new Vector3(-18, YHeight, 0) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.AMPOL, Rotation = 90, Direction = 1, Steps = 1, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 262.5f, Centre = new Vector3(-18, YHeight, 3) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WESTERN_MINING, Rotation = 90, Direction = 1, Steps = -2, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 255f, Centre = new Vector3(-18, YHeight, 6) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WOOLWORTHS, Rotation = 90, Direction = 1, Steps = 3, Dividend = 4, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 247.5f, Centre = new Vector3(-18, YHeight, 9) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Rotation = 90, Direction = 1, Steps = -4, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 240f, Centre = new Vector3(-18, YHeight, 12) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.ALCOA, Rotation = 90, Direction = 1, Steps = 5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 232.5f, Centre = new Vector3(-18, YHeight, 15) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CORNER, Rotation = 135, Direction = -1, Steps = -20, Dividend = 0, Fee = 0.1f, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 225f, Centre = new Vector3(-18, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BHP, Rotation = 180, Direction = -1, Steps = -5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 217.5f, Centre = new Vector3(-15, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.AMPOL, Rotation = 180, Direction = -1, Steps = 4, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 210f, Centre = new Vector3(-12, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WESTERN_MINING, Rotation = 180, Direction = -1, Steps = -3, Dividend = 1, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 202.5f, Centre = new Vector3(-9, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WOOLWORTHS, Rotation = 180, Direction = 1, Steps = 2, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 195f, Centre = new Vector3(-6, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Rotation = 180, Direction = 1, Steps = -1, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 187.5f, Centre = new Vector3(-3, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.START, Rotation = 180, Direction = 0, Steps = 0, Dividend = 0, Fee = 100, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 180f, Centre = new Vector3(0, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BANK_OF_NSW, Rotation = 180, Direction = 1, Steps = -1, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 172.5f, Centre = new Vector3(3, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.ALCOA, Rotation = 180, Direction = 1, Steps = 2, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 165f, Centre = new Vector3(6, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BHP, Rotation = 180, Direction = 1, Steps = -3, Dividend = 1, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 157.5f, Centre = new Vector3(9, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.COLES, Rotation = 180, Direction = 1, Steps = 4, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 150f, Centre = new Vector3(12, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WESTERN_MINING, Rotation = 180, Direction = 1, Steps = -5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 142.5f, Centre = new Vector3(15, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CORNER, Rotation = 225, Direction = -1, Steps = 20, Dividend = 0, Fee = 0.1f, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 135f, Centre = new Vector3(18, YHeight, 18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BANK_OF_NSW, Rotation = 270, Direction = -1, Steps = 5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 127.5f, Centre = new Vector3(18, YHeight, 15) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WOOLWORTHS, Rotation = 270, Direction = -1, Steps = -4, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 120f, Centre = new Vector3(18, YHeight, 12) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Rotation = 270, Direction = -1, Steps = 3, Dividend = 3, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 112.5f, Centre = new Vector3(18, YHeight, 9) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.AMPOL, Rotation = 270, Direction = 1, Steps = -2, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 105f, Centre = new Vector3(18, YHeight, 6) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WESTERN_MINING, Rotation = 270, Direction = 1, Steps = 1, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 97.5f, Centre = new Vector3(18, YHeight, 3) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.START, Rotation = 270, Direction = 0, Steps = 0, Dividend = 0, Fee = 100, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 90f, Centre = new Vector3(18, YHeight, 0) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BHP, Rotation = 270, Direction = 1, Steps = 1, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 82.5f, Centre = new Vector3(18, YHeight, -3) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.COLES, Rotation = 270, Direction = 1, Steps = -2, Dividend = 2, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 75f, Centre = new Vector3(18, YHeight, -6) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BANK_OF_NSW, Rotation = 270, Direction = 1, Steps = 3, Dividend = 3, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 67.5f, Centre = new Vector3(18, YHeight, -9) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.ALCOA, Rotation = 270, Direction = 1, Steps = -4, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 60f, Centre = new Vector3(18, YHeight, -12) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Rotation = 270, Direction = 1, Steps = 5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 52.5f, Centre = new Vector3(18, YHeight, -15) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CORNER, Rotation = 315, Direction = -1, Steps = -20, Dividend = 0, Fee = 0.1f, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 45f, Centre = new Vector3(18, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.AMPOL, Rotation = 0, Direction = -1, Steps = -5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 37.5f, Centre = new Vector3(15, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WESTERN_MINING, Rotation = 0, Direction = -1, Steps = 4, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 30f, Centre = new Vector3(12, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.COLES, Rotation = 0, Direction = -1, Steps = -3, Dividend = 2, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 22.5f, Centre = new Vector3(9, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Rotation = 0, Direction = 1, Steps = 2, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 15f, Centre = new Vector3(6, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.WOOLWORTHS, Rotation = 0, Direction = 1, Steps = -1, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 7.5f, Centre = new Vector3(3, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.START, Rotation = 0, Direction = 0, Steps = 0, Dividend = 0, Fee = 100, Meeting = false, MustSell = false, CanBuy = false, ViewingAngle = 0f, Centre = new Vector3(0, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.ALCOA, Rotation = 0, Direction = 1, Steps = -1, Dividend = 4, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 352.5f, Centre = new Vector3(-3, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BANK_OF_NSW, Rotation = 0, Direction = 1, Steps = 2, Dividend = 3, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 345f, Centre = new Vector3(-6, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.AMPOL, Rotation = 0, Direction = 1, Steps = -3, Dividend = 2, Fee = 0, Meeting = true, MustSell = false, CanBuy = true, ViewingAngle = 337.5f, Centre = new Vector3(-9, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.BHP, Rotation = 0, Direction = 1, Steps = 4, Dividend = 1, Fee = 0, Meeting = false, MustSell = false, CanBuy = true, ViewingAngle = 330f, Centre = new Vector3(-12, YHeight, -18) });
        TradeSquares.Add(new TradingSquare { Company = CompanyEnum.COLES, Rotation = 0, Direction = 1, Steps = -5, Dividend = 0, Fee = 0, Meeting = false, MustSell = true, CanBuy = false, ViewingAngle = 322.5f, Centre = new Vector3(-15, YHeight, -18) });


        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 1, Centre = new Vector3(-15.75f, YHeight, -9), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, -9), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 1, Centre = new Vector3(-14.25f, YHeight, -6), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, -3), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 3, Centre = new Vector3(-14.25f, YHeight, 0), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, 3), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 1, Centre = new Vector3(-14.25f, YHeight, 6), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, 9), Offset = 3 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.ALCOA, Multiplier = 1, Centre = new Vector3(-15.75f, YHeight, 9), Offset = 3 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 1, Centre = new Vector3(15.75f, YHeight, -9), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, -9), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 3, Centre = new Vector3(14.25f, YHeight, -6), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, -3), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 1, Centre = new Vector3(14.25f, YHeight, 0), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, 3), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 3, Centre = new Vector3(14.25f, YHeight, 6), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, 9), Offset = 33 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BANK_OF_NSW, Multiplier = 1, Centre = new Vector3(15.75f, YHeight, 9), Offset = 33 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 1, Centre = new Vector3(-9, YHeight, -15.75f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 2, Centre = new Vector3(-9, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 3, Centre = new Vector3(-6, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 2, Centre = new Vector3(-3, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 3, Centre = new Vector3(0, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 2, Centre = new Vector3(3, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 3, Centre = new Vector3(6, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 2, Centre = new Vector3(9, YHeight, -14.25f), Offset = 45 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.AMPOL, Multiplier = 1, Centre = new Vector3(9, YHeight, -15.75f), Offset = 45 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 1, Centre = new Vector3(9, YHeight, 15.75f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 3, Centre = new Vector3(9, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 2, Centre = new Vector3(6, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 3, Centre = new Vector3(3, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 2, Centre = new Vector3(0, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 3, Centre = new Vector3(-3, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 2, Centre = new Vector3(-6, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 3, Centre = new Vector3(-9, YHeight, 14.25f), Offset = 21 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.BHP, Multiplier = 1, Centre = new Vector3(-9, YHeight, 15.75f), Offset = 21 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 1, Centre = new Vector3(-9, YHeight, 15.75f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 3, Centre = new Vector3(-9, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 2, Centre = new Vector3(-6, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 3, Centre = new Vector3(-3, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 2, Centre = new Vector3(0, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 3, Centre = new Vector3(3, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 2, Centre = new Vector3(6, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 3, Centre = new Vector3(9, YHeight, 14.25f), Offset = 15 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WESTERN_MINING, Multiplier = 1, Centre = new Vector3(9, YHeight, 15.75f), Offset = 15 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 1, Centre = new Vector3(9, YHeight, -15.75f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 2, Centre = new Vector3(9, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 3, Centre = new Vector3(6, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 2, Centre = new Vector3(3, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 3, Centre = new Vector3(0, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 2, Centre = new Vector3(-3, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 3, Centre = new Vector3(-6, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 2, Centre = new Vector3(-9, YHeight, -14.25f), Offset = 39 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.COLES, Multiplier = 1, Centre = new Vector3(-9, YHeight, -15.75f), Offset = 39 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 1, Centre = new Vector3(15.75f, YHeight, 9), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, 9), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 3, Centre = new Vector3(14.25f, YHeight, 6), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, 3), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 1, Centre = new Vector3(14.25f, YHeight, 0), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, -3), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 3, Centre = new Vector3(14.25f, YHeight, -6), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 2, Centre = new Vector3(14.25f, YHeight, -9), Offset = 27 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.CONSOLIDATED_PRESS, Multiplier = 1, Centre = new Vector3(15.75f, YHeight, -9), Offset = 27 });

        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 1, Centre = new Vector3(-15.75f, YHeight, 9), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, 9), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 1, Centre = new Vector3(-14.25f, YHeight, 6), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, 3), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 3, Centre = new Vector3(-14.25f, YHeight, 0), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, -3), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 1, Centre = new Vector3(-14.25f, YHeight, -6), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 2, Centre = new Vector3(-14.25f, YHeight, -9), Offset = 9 });
        MeetingSquares.Add(new MeetingSquare { Company = CompanyEnum.WOOLWORTHS, Multiplier = 1, Centre = new Vector3(-15.75f, YHeight, -9), Offset = 9 });

        WorkSquares.Add(new WorkSquare { Job = WorkEnum.PROSPECTOR, Name = "Prospector", Salary = 400, Centre = new Vector3(-13.5f, YHeight, -13.5f), WinRolls = new List<int> { 2, 12 } });
        WorkSquares.Add(new WorkSquare { Job = WorkEnum.DEEP_SEA_DIVER, Name = "Deep Sea Diver", Salary = 300, Centre = new Vector3(-13.5f, YHeight, 13.5f), WinRolls = new List<int> { 3, 11 } });
        WorkSquares.Add(new WorkSquare { Job = WorkEnum.DOCTOR, Name = "Doctor", Salary = 200, Centre = new Vector3(13.5f, YHeight, 13.5f), WinRolls = new List<int> { 4, 10 } });
        WorkSquares.Add(new WorkSquare { Job = WorkEnum.POLICE_OFFICER, Name = "Police Officer", Salary = 100, Centre = new Vector3(13.5f, YHeight, -13.5f), WinRolls = new List<int> { 5, 9 } });



    }


    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        //boardHolder = new GameObject("Board").transform;

        //foreach (TradingSquare square in TradeSquares)
        //{
        //    if (square.Company == CompanyEnum.AMPOL)

        //    {
        //        GameObject toInstantiate = Players[Random.Range(0, Players.Length)];

        //        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        //        GameObject instance =
        //            Instantiate(toInstantiate, square.Centre, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        //        Debug.Log("Added player at " + square.Centre.ToString());
        //        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        //        instance.transform.SetParent(boardHolder);
        //    }
        //}

        //foreach (MeetingSquare square in MeetingSquares)
        //{
        //    GameObject toInstantiate = Players[Random.Range(0, Players.Length)];

        //    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        //    GameObject instance =
        //        Instantiate(toInstantiate, square.Centre, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        //    Debug.Log("Added player at " + square.Centre.ToString());
        //    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        //    instance.transform.SetParent(boardHolder);
        //}

        //foreach (WorkSquare square in WorkSquares)
        //{
        //    GameObject toInstantiate = Players[Random.Range(0, Players.Length)].GetComponentInChildren<;

        //    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
        //    GameObject instance =
        //        Instantiate(toInstantiate, square.Centre, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        //    Debug.Log("Added player at worksquare " + square.Centre.ToString());
        //    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        //    instance.transform.SetParent(boardHolder);
        //}
    }


    ////RandomPosition returns a random position from our list gridPositions.
    //Vector3 RandomPosition()
    //{
    //    //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
    //    int randomIndex = Random.Range(0, TradeSquares.Count);

    //    //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
    //    Vector3 randomPosition = TradeSquares[randomIndex];

    //    //Remove the entry at randomIndex from the list so that it can't be re-used.
    //    TradeSquares.RemoveAt(randomIndex);

    //    //Return the randomly selected Vector3 position.
    //    return randomPosition;
    //}


    ////LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    //{
    //    //Choose a random number of objects to instantiate within the minimum and maximum limits
    //    int objectCount = Random.Range(minimum, maximum + 1);

    //    //Instantiate objects until the randomly chosen limit objectCount is reached
    //    for (int i = 0; i < objectCount; i++)
    //    {
    //        //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
    //        Vector3 randomPosition = RandomPosition();

    //        //Choose a random tile from tileArray and assign it to tileChoice
    //        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

    //        //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
    //        Instantiate(tileChoice, randomPosition, Quaternion.identity);
    //    }
    //}


    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene()
    {
        //Reset our list of gridpositions.
        InitialiseList();

        //Creates the outer walls and floor.
        BoardSetup();

        //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

        ////Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        ////Determine number of enemies based on current level number, based on a logarithmic progression
        //int enemyCount = (int)Mathf.Log(level, 2f);

        ////Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(Slider, new Vector3(0, 1, 0), Quaternion.AngleAxis(-90, Vector3.up));
    }
}