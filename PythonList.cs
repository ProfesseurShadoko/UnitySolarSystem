

public class PythonList {

    private int cardinal;
    private CelestialObject[] elements;

    public PythonList() {
        this.cardinal=0;
        this.elements=new CelestialObject[1];
    }

    public void append(CelestialObject Planet) {
        this.elements[cardinal]=Planet;
        this.cardinal++;

        if (this.elements.Length==this.cardinal) {
            CelestialObject[] tmp = this.elements;
            this.elements=new CelestialObject[tmp.Length*2];
            for (int i=0; i<this.cardinal;i++) {
                this.elements[i]=tmp[i];
            }
        }
    }

    public int length() {
        return this.cardinal;
    }

    public CelestialObject element(int i) {
        return this.elements[i];
    }

    public CelestialObject pop() {
        this.cardinal = this.cardinal-1;
        return this.elements[this.cardinal];
    }

    public override string ToString() {
        string output="[ ";
        for (int i=0; i<this.cardinal; i++) {
            output=output+this.element(i)+" , ";
        }
        return output + "]";
    }
}
