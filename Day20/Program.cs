public static int getTotalX(List<int> a, List<int> b)
    {
        int count=0;
        int start = a.Max();
        int end=b.Min();
        for(int i=start;i<=end;i++){
            bool isValid = true;
            foreach(var num in a){
                if(i%num!=0){
                    isValid=false;
                    break;
                }
            }
             if (isValid){
            foreach(var num in b){
                if(num%i!=0){
                    isValid=false;
                    break;
                }
            }
             }
             if (isValid){
            count++;}
        }
        return count;
    }