using System;

namespace AdventOfCode._2019.Day12
{
    class Moon
    {
        private (int X, int Y, int Z) position;
        private readonly (int X, int Y, int Z) startPosition;
        private (int X, int Y, int Z) velocity;

        public int KineticEnergy
            => Math.Abs(velocity.X) + Math.Abs(velocity.Y) + Math.Abs(velocity.Z);

        public int PotentialEnergy
            => Math.Abs(position.X) + Math.Abs(position.Y) + Math.Abs(position.Z);

        public int TotalEnergy
            => KineticEnergy * PotentialEnergy;
                
        public Moon(int posX, int posY, int posZ)
        {
            position = (posX, posY, posZ);
            startPosition = (posX, posY, posZ);
            velocity = (0, 0, 0);
        }

        public Moon(Moon moon)
        {
            position = moon.position;
            velocity = moon.velocity;
        }

        public void ApplyVelocity()
        {
            ApplyVelocityForAxisX();
            ApplyVelocityForAxisY();
            ApplyVelocityForAxisZ();
        }

        public void ApplyVelocityForAxisX()
        {
            position.X += velocity.X;
        }

        public void ApplyVelocityForAxisY()
        {            
            position.Y += velocity.Y;
        }

        public void ApplyVelocityForAxisZ()
        {
            position.Z += velocity.Z;
        }

        public void ApplyGravity(Moon other)
        {
            ApplyGravityForAxisX(other);
            ApplyGravityForAxisY(other);
            ApplyGravityForAxisZ(other);
        }

        public void ApplyGravityForAxisX(Moon other)
        {
            if (position.X > other.position.X)
            {
                velocity.X--;
            }
            else if (position.X < other.position.X)
            {
                velocity.X++;
            }
        }

        public void ApplyGravityForAxisY(Moon other)
        {
            if (position.Y > other.position.Y)
            {
                velocity.Y--;
            }
            else if (position.Y < other.position.Y)
            {
                velocity.Y++;
            }
        }

        public void ApplyGravityForAxisZ(Moon other)
        {
            if (position.Z > other.position.Z)
            {
                velocity.Z--;
            }
            else if (position.Z < other.position.Z)
            {
                velocity.Z++;
            }
        }

        public bool IsAtStartOnAxisX()
        {
            return position.X == startPosition.X;
        }

        public bool IsAtStartOnAxisY()
        {
            return position.Y == startPosition.Y;
        }

        public bool IsAtStartOnAxisZ()
        {
            return position.Z == startPosition.Z;
        }

        public override string ToString()
        {
            return $"pos=<x={position.X}, y={position.Y}, z={position.Z}>, vel=<x={velocity.X}, y={velocity.Y}, z={velocity.Z}>";
        }

        public override bool Equals(object obj)
        {
            return obj is Moon other &&
                position == other.position &&
                velocity == other.velocity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(position, velocity);
        }
    }
}
