using JuMP
using Cbc
using Dates

function readData()
    arr = Array{Int, 2}(undef, 0,0 )
    if isfile("instances/temp.txt")
        myFile = open("instances/temp.txt")
        data = readlines(myFile)
        n = length(data)
        arr = fill(0,(n - 3, n -10))

        i = 0
        for line in data
            if line == ""
                continue
            end
            i += 1
            var = split(line, " ")
            for j in 1:length(var)
                arr[i, j] = parse(Int, var[j])
            end
        end
    end
    return arr
end

function square(n::Int64)
    return n*n
end

function solve_TSP(d::Array{Int, 2})
    numOfAero = d[1,1]#number of aerodromes
    start = d[2,1]
    endP = d[3,1]
    numOfMust = d[4,1]#number of points must be visited
    numOfRegion = d[5,1]#number of regions
    maxDistance = d[7,1]#max distance travel without landing
    
    tempstart = start
    tempend = endP

    lines = size(d, 1) #return the number of lines of array d
    
    # let startPoint at the first, lastPoint at the last
    for j in 1:2
        temp = d[start+7,j]
        d[start+7,j] = d[8,j]
        d[8,j] = temp
        temp = d[endP+7,j]
        d[endP+7,j] = d[numOfAero+7,j]
        d[numOfAero+7,j] = temp
    end
    #n -> the size of the coordinate matrix
    n = lines - 7

    regOfEach = Array{Int}(zeros(n))#an array used to store the information of region for each aerodrome
    for j in 1:n
        regOfEach[j] = d[6,j]
    end

    #because of the swap between the first point and the end point, the position of the region array need to be changed correspondingly
    temp = regOfEach[1] #temp is used to store the value needed to be swapped
    regOfEach[1] = regOfEach[start]
    regOfEach[start] = temp

    temp = regOfEach[numOfAero]
    regOfEach[numOfAero] = regOfEach[endP]
    regOfEach[endP] = temp

    start = 1#reset the start and end point
    endP = numOfAero

    disArr = Array{Int, 2}(undef, n, n)#an array used to store the information of the distance between each point
    for i in 1:n
        for j in 1:n
            disArr[i, j] = round(sqrt(square(d[i+7,1]-d[j+7,1]) + square(d[i+7,2]-d[j+7,2])))
        end
    end

    Model
    m = Model(Cbc.Optimizer)
    set_optimizer_attribute(m, "threads", 16)

    #Variables
    @variable(m, x[i in 1:n, j in 1:n], Bin)
    @variable(m, visited[i in 1:n], Bin)
    @variable(m, u[j in 1:n] >= 0)

    #Objective function
    @objective(m, Min, sum(disArr[i,j]*x[i,j] for i in 1:n, j in 1:n))

    #Constraints for preventing self-loop
    @constraint(m, loopsForbidden[i in 1:n], x[i,i] == 0)

    #start from startPoint
    @constraint(m, fromStartOut, sum(x[start, j] for j in 1:n) == 1)
    @constraint(m, fromStartIn, sum(x[i, start] for i in 1:n) == 0)
    @constraint(m, visited[1] == 1)

    #end at endPoint
    @constraint(m, toEndOut, sum(x[endP, j] for j in 1:n) == 0)
    @constraint(m, toEndIn, sum(x[i, endP] for i in 1:n) == 1)
    @constraint(m, visited[numOfAero] == 1)

    #number of point must be visited
    @constraint(m, sum(x[i,j] for i in 1:n, j in 1:n) >= numOfMust-1)

    #the max distance travel without landing
    @constraint(m, maxDis[i in 1:n, j in 1:n], x[i,j]*disArr[i,j] <= maxDistance)

    #constraint for middle points
    #each middle point could only have one out-degree
    @constraint(m, noMoreThanOne1[i in 2:n-1], sum(x[i,j] for j in 2:n) <= 1)
    #each middle point could only have one in-degree
    @constraint(m, noMoreThanOne2[j in 2:n-1], sum(x[i,j] for i in 1:n-1) <= 1)
    #build the relation between variable x and visited, and ensure that in-degree == out-degree
    @constraint(m, middlePoint1[i in 2:n-1], sum(x[i,j] for j in 1:n) == visited[i])
    @constraint(m, middlePoint2[j in 2:n-1], sum(x[i,j] for i in 1:n) == visited[j])

    #region constraint that ensure that each region must be visited once
    @constraint(m, [i in 2:n-1], visited[i] == sum(x[i,j] for j in 1:n))
    @constraint(m, region1[i in 1:numOfRegion], sum(visited[j] for j in 1:n if regOfEach[j] == i) >= 1)

    #MTZ
    @constraint(m, mtz[i in 1:n, j in 2:n], u[j] >= u[i]+1-n+n*x[i,j]+(n-2)*x[j,i])
    @constraint(m, mtz2[j in 1:n], u[j] <= n)
    @constraint(m, mtz3[j in 1:n], u[j] >= 1)

    #Solve the problem
    set_silent(m)
    optimize!(m)
    
    # return to origin for the proposed outcome
    for i in 1:n
        temp = x[i,tempstart]
        x[i,tempstart] = x[i,start]
        x[i,start] = temp
        temp = x[tempstart,i]
        x[tempstart,i] = x[start,i]
        x[start,i] = temp
        temp = x[i,tempend]
        x[i,tempend] = x[i,endP]
        x[i,endP] = temp
        temp = x[tempend,i]
        x[tempend,i] = x[endP,i]
        x[endP,i] = temp
    end
    println("Result - Optimal solution found")
    println("")
    println(" Distance travelled = ", JuMP.objective_value(m))
    print("Edges used in the solutin :")
    for i in 1:n
        for j in 1:n
            valij =  JuMP.value(x[i,j])
            if valij <= 1.000001 && valij >= 0.999999
                print(i, "->", j, " ")
            end
        end
    end
    println()

end

d1=now(Dates.UTC)
D = readData()
solve_TSP(D)
d2=now(Dates.UTC)
println(d2-d1)
