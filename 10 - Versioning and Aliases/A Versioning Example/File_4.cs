// 11 - Versioning\A Versioning Example
// copyright 2000 Eric Gunnerson
class Control
{
    public virtual void Foo() {}
}
class MyControl: Control
{
    // not an override
    public new virtual void Foo() {}
}