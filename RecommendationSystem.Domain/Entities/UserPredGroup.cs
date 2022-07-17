using System;

namespace RecommendationSystem.Domain.Entities
{
    public class UserPredGroup : BaseEntity
    {
        public Guid UserId { get; set; }
        public int FirstGroup { get; set; }
        public float FirstGroupScore { get; set; }
        public int SecondGroup { get; set; }
        public float SecondGroupScore { get; set; }
        public int ThirdGroup { get; set; }
        public float ThirdGroupScore { get; set; }
        public int FourthGroup { get; set; }
        public float FourthGroupScore { get; set; }
        public int FifthGroup { get; set; }
        public float FifthGroupScore { get; set; }

        public float[] ConvertToArray()
        {
            return new[] {
                FirstGroup,
                FirstGroupScore,
                SecondGroup,
                SecondGroupScore,
                ThirdGroup,
                ThirdGroupScore,
                FourthGroup,
                FourthGroupScore,
                FifthGroup,
                FifthGroupScore
            };
        }
    }
}
