<template>
  <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@48,400,0,0" />
  <h1>Todo List App - Sangjin</h1>
  <div>
    <input id="addInput" v-model="addInputText" type="text" placeholder="Write here!!!" class="write">
    <button @click="onAdd" class="my-btn" >Add</button>
  </div>
  <ul>
    <li v-for="todoItem of list" v-bind:key="todoItem" >
      <span class="material-symbols-outlined done-btn" @click="onSetDone(todoItem)">
        {{ todoItem.isDone ? 'task_alt' : 'circle' }}
      </span>
      <input type="text" v-model="todoItem.content" @keyup="onKeyupModify($event, todoItem)" :class="'read '+(todoItem.isDone ? 'done' : '')"/>
      <!-- <span>{{ todoItem }}</span> -->
      <button @click="onDel(todoItem.id)" class="my-btn" >Delete</button>
    </li>
  </ul>
</template>

<script>
import _ from 'lodash'
// const API_URL = `http://localhost:53015`
const API_URL = `https://localhost:44397`

let modifyTimeout

export default {
  data () {
    return {
      list: null,
      addInputText: ''
    }
  },
  created () {
    // fetch on init
    this.fetchData()
  },
  methods: {
    async fetchData () {
      const url = `${API_URL}/api/todo/list`
      const res = await fetch(url, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      const json = await res.json()
      this.list = json
    },
    async addTodo (content) {
      const url = `${API_URL}/api/todo`
      const res = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          content: content
        })
      })
      const json = await res.json()
      return json
    },
    async modifyTodo (id, content) {
      const url = `${API_URL}/api/todo/${id}`
      await fetch(url, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          content: content
        })
      })
    },
    async setDone (id, done) {
      const url = `${API_URL}/api/todo/${done ? 'done' : 'undo'}/${id}`
      await fetch(url, {
        method: 'PUT'
      })
    },
    async deleteTodo (id) {
      const url = `${API_URL}/api/todo/${id}`
      await fetch(url, {
        method: 'DELETE'
      })
    },
    async onAdd (e) {
      const newTodo = await this.addTodo(this.addInputText)
      this.list.unshift(newTodo)
      this.addInputText = '' // add ?????? call ??????, input ?????????
    },
    onKeyupModify (e, todoItem) {
      clearTimeout(modifyTimeout)
      modifyTimeout = setTimeout(async () => {
        const content = e.target.value
        await this.modifyTodo(todoItem.id, content)
      }, 1000)
    },
    async onDel (id) {
      _.remove(this.list, (item) => {
        return item.id === id
      })
      await this.deleteTodo(id)
    },
    async onSetDone (item) {
      const newDone = !item.isDone
      item.isDone = newDone
      await this.setDone(item.id, newDone)
    }
  }
}
</script>

<style scoped>
ul {
  padding-inline-start: 0;
}
li {
  /* border-color: limegreen;
  border-width: 2px;
  border-style: solid; */
  margin-bottom: 1rem;
  list-style: none;
}
.done-btn {
  background-color: rgba(0,0,0,0);
  cursor: pointer;
  vertical-align: bottom;
  border-radius: 50%;
}
.done-btn:hover {
  background-color: rgba(0,0,0,0.1);
}
.write{
  margin-right: 1rem;
}
.read{
  margin-right: 1rem;
  margin-left: 0.5rem;
  border-width: 0;
  height: 1.5rem;
  font-weight: 600;
}
.read.done{
  text-decoration: line-through;
  color: rgb(50, 50, 50, 0.5);
}
.my-btn {
  background-color: rgb(140, 255, 160); /* lime green=(50,205,70) */
  color: rgb(80,80,80);
  font-weight: 700;
  cursor: pointer;
  border-width: 0;
  border-radius: 1rem;
  height: 1.5rem;
}
</style>
