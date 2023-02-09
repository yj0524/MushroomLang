package com.github.yj0524

import java.io.File
import java.io.IOException
import java.nio.ByteBuffer
import java.util.*
import kotlin.system.exitProcess

val memory: MutableList<Int> = mutableListOf()

var pc: Int = 0

fun MutableList<Int>.getSafe(index: Int): Int {
    return if(index > this.lastIndex) 0 else this[index]
}

fun MutableList<Int>.setSafe(index: Int, value: Int) {
    if(index > this.lastIndex){
        for(i in 0 until index - this.lastIndex){
            this.add(0)
        }
    }
    this[index] = value
}

fun parseLine(code: String){
    if(code.startsWith("브섯!")){
        exitProcess(memory.getSafe(code.replace("브섯!", "").toUmmInt()))
    }
    else if(code.endsWith("균?")){
        if(code.replace("균?", "") == ""){
            throw java.lang.RuntimeException("문법 오류: 대입할 변수 미지정 (${pc + 1}번 줄)")
        }
        else {
            val sc = Scanner(System.`in`)
            memory.setSafe(code.replace("균?", "").toUmmInt(), sc.nextInt())
        }
    }
    else if(code.startsWith("균")){
        if(code.endsWith("!")){
            val q = code.replace("균", "").replace("!", "")
            print("${q.toUmmInt()}")
        }
        else if(code.endsWith("ㅋ")){
            val q = code.replace("균", "").replace("ㅋ", "")
            if(q == ""){
                println()
            }
            else {
                val b = q.toUmmInt()

                print(convertUnicodeToString("\\u${b.toString(16).padStart(4 - b.toString(16).length, '0')}"))
            }
        }
    }
    else if(code.startsWith("포자")){
        if(code.contains("?")){
            val cc = code.replace("포자", "").split("?")

            val r = cc[0].toUmmInt()

            if(r == 0){
                parseLine(cc[1])
            }
        }
    }
    else if(code.startsWith("섯")){
        if(code.replace("섯", "") == ""){
            throw java.lang.RuntimeException("문법 오류: 점프할 줄 번호 미지정 (${pc + 2}번 줄)")
        }
        else {
            val lPC = code.replace("섯", "")

            pc = lPC.toUmmInt() - 3
        }
    }
    else if(code.contains("버")){
        val c = code.split("버")

        memory.setSafe(c[0].countChar("어"), c[1].toUmmInt())
    }
    else {
        return
    }
    return
}

fun main(args: Array<String>) {
    if(args.isEmpty()){
        throw IOException("어떻게 파일 이름이 없냐ㅋㅋ")
    }
    else if(!args[0].endsWith(".mushroom")){
        throw IOException("어떻게 ${args[0]}이 파일이름이냐ㅋㅋ")
    }

    val f = File(args[0])
    if(!f.canRead()) {
        throw IOException("파일을 읽을 수 없음")
    }
    else {
        var codes = f.readText().trim().replace("~", "\n").split("\n")

        if(codes[0] != "어떻게" && codes.last() != "이 균이냐ㅋㅋ"){
            throw Exception("어떻게 이게 버섯랭이냐ㅋㅋ")
        }

        codes = codes.subList(1, codes.size)

        while(true){
            if(codes[pc] != "이 균이냐ㅋㅋ"){
                parseLine(codes[pc].trim())
                pc++
            }
            else break
        }
    }

    exitProcess(0)
}