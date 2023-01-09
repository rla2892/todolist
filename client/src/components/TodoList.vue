<template>
  <h1>Todo List App - Sangjin</h1>
  <div>
    <input id="addInput" v-model="addInputText" type="text" placeholder="Write here!!!">
    <button @click="onAdd" >Add</button>
  </div>
  <ul>
    <li v-for="todoItem of list" v-bind:key="todoItem" >
      <button @click="onSetDone(todoItem)" class="doneBtn">{{ todoItem.isDone ? 'V' : '&nbsp;' }}</button>
      <input type="text" v-model="todoItem.content" @keyup="onKeyupModify($event, todoItem)" />
      <span>{{ todoItem }}</span>
      <button @click="onDel(todoItem.id)" >Delete</button>
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
      this.addInputText = '' // add 함수 call 이후, input 비우기
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
  border-color: limegreen;
  border-width: 2px;
  border-style: solid;
  margin-bottom: 1rem;
  list-style: none;
}
.doneBtn {
  background-color: rgba(0,0,0,0);
  cursor: pointer;
}
.doneBtn:hover {
  background-color: rgba(0,0,0,0.1);
}
</style>
