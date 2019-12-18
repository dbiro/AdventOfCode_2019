using System;

namespace AdventOfCode._2019.Day12
{
    class Moon
    {
        private (int X, int Y, int Z) position;
        private (int X, int Y, int Z) velocity;

        public int KineticEnergy
            => Math.Abs(velocity.X) + Math.Abs(velocity.Y) + Math.Abs(velocity.Z);

        public int PotentialEnergy
            => Math.Abs(position.X) + Math.Abs(position.Y) + Math.Abs(position.Z);

        public int TotalEnergy
            => KineticEnergy * PotentialEnergy;

        public Moon(int posX, int posY, int posZ)
        {
            this.position = (posX, posY, posZ);
            velocity = (0, 0, 0);
        }

        public void ApplyVelocity()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            position.Z += velocity.Z;
        }

        public void ApplyGravity(Moon other)
        {            
            if (position.X > other.position.X)
            {
                velocity.X--;
            }
            else if (position.X < other.position.X)
            {
                velocity.X++;
            }

            if (position.Y > other.position.Y)
            {
                velocity.Y--;
            }
            else if (position.Y < other.position.Y)
            {
                velocity.Y++;
            }

            if (position.Z > other.position.Z)
            {
                velocity.Z--;
            }
            else if (position.Z < other.position.Z)
            {
                velocity.Z++;
            }
        }

        public override string ToString()
        {
            return $"pos=<x={position.X}, y={position.Y}, z={position.Z}>, vel=<x={velocity.X}, y={velocity.Y}, z={velocity.Z}>";
        }
    }
}
