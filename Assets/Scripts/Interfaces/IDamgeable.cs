using UnityEngine;

public interface IDamgeable:Ihitable
{
    void TakeDamge(Vector2 damgeDir,int damgeAmout,float knockbackThurst);
}