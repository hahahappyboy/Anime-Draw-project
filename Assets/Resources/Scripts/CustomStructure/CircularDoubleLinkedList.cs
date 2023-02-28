namespace CustomStructure {
    /// <summary>
    /// 循环链表，用于实现档案循环播放
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CircularDoubleLinkedListNode<T> {
        //存放的数据
        private T data;
        //前一个节点
        private CircularDoubleLinkedListNode<T> previous;
        public CircularDoubleLinkedListNode<T> Previous {
            get { return previous; }
            set { previous = value; }
        }
        //后一个节点
        private CircularDoubleLinkedListNode<T> next;
        public CircularDoubleLinkedListNode<T> Next {
            get { return next; }
            set { next = value; }
        }
        public T Value {
            get { return data; }
            set { data = value; }
        }

        public CircularDoubleLinkedListNode(T data, CircularDoubleLinkedListNode<T> next, CircularDoubleLinkedListNode<T> previous) {
            this.data = data;
            this.next = next;
            this.previous = previous;
        }
        public CircularDoubleLinkedListNode(T data)
        {
            this.data = data;//即各种类型的默认值
            this.next = null;
            this.previous = null;
        }
    }
    class CircularDoubleLinkedList<T>{
    //头节点
    private CircularDoubleLinkedListNode<T> head;
    public CircularDoubleLinkedListNode<T> First {
        get { return head; }
        set { head = value; }
    }
    public CircularDoubleLinkedListNode<T> Last {
        get {
            if (head != null) return head.Next;
            else return null;
        }
    }
    //链表大小
    private int count;
    public int Count {
        get { return count; }
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="head">头节点</param>
    public CircularDoubleLinkedList() {
        this.head = null;
    }
    /// <summary>
    /// 链表是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() {
        if (this.count == 0 || this.head == null) return true;
        return false;
    }
    public void Clear() {
        this.head = null;
        count = 0;
    }
    /// <summary>
    /// 链表末尾加入节点
    /// </summary>
    /// <param name="data"></param>
    public void AddLast(CircularDoubleLinkedListNode<T> node) {
        this.count++;
        //没有头结点
        if (this.head == null) {
            this.head = node;
            this.head.Next = this.head;
            this.head.Previous = this.head;
            this.count++;
            return;
        }
        //有头结点
        //拿到尾节点
        var lastNode = this.head.Previous;
        //插入
        node.Previous = lastNode;
        node.Next = head;
        lastNode.Next = node;
        head.Previous = node;
    }
    /// <summary>
    /// 从开始加入
    /// </summary>
    /// <param name="node"></param>
    public void AddFirst(CircularDoubleLinkedListNode<T> node) {
        this.count++;
        //没有头结点
        if (this.head == null) {
            this.head = node;
            this.head.Next = this.head;
            this.head.Previous = this.head;
      
            return;
        }
        //有头结点
        //拿到尾节点
        var lastNode = this.head.Previous;
        //插入
        node.Previous = lastNode;
        node.Next = this.head;
        this.head.Previous = node;
        lastNode.Next = node;
        this.head = node;
 
    }
        
}
}