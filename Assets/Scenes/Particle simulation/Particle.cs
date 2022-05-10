public class Particle
{
    public Particle(ushort x, ushort y, byte type) {
        this.x = x;
        this.y = y;

        this.type = type;
    }
    public byte type;
    public ushort x, y;
    public short vX = 0, vY = 0;
}