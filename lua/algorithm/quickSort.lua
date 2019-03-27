dump = function(tab)
    local str = table.concat(tab, " ")
    print(str)
end

local Swap = function(arr, i, j)
    arr[i], arr[j] = arr[j], arr[i]
    -- dump(arr)
end

local Partition = function(arr, left, right)
    pivot = arr[right]
    tail = left - 1

    for i = left, right - 1 do
        if arr[i] <= pivot then
            tail = tail + 1
            Swap(arr, tail, i)
        end
    end
    Swap(arr, tail + 1, right)
    return tail + 1
end

QuickSort = function(arr, left, right)
    if left >= right then
        return
    end
    pivot_index = Partition(arr, left, right)
    QuickSort(arr, left, pivot_index - 1)
    QuickSort(arr, pivot_index + 1, right)
end


tab  = { 5, 2, 9, 4, 7, 6, 1, 3, 8 }; 

QuickSort(tab, 1,#tab);
 
dump(tab)
