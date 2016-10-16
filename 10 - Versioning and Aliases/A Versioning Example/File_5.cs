// 11 - Versioning\A Versioning Example
// copyright 2000 Eric Gunnerson
class Control
{
    public virtual void Foo() {}
}
class MyControl: Control
{
    // an override for Control.Foo()
    public override void Foo() {}
}